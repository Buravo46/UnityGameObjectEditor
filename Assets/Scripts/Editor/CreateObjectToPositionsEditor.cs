using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
/*===============================================================*/
/**
* 設定したPrefabを,設定した複数の座標に生成するエディタ
* 2014年12月25日 Buravo
*/ 
public class CreateObjectToPositionsEditor : EditorWindow 
{

    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief 親ゲームオブジェクト
    */
    private GameObject m_parent;
    /**
    * @brief 生成するオブジェクトのプレハブ
    */
    private GameObject m_prefab;
    /**
    * @brief 生成する個数
    */
    private int m_create_num = 0;
    /**
    * @brief 座標リスト
    */
    private List<Vector3> m_position_list;
    /**
    * @brief スクロール座標
    */
    private Vector2 m_scroll_position;
    /*===============================================================*/
    #endregion 
 
    /*===============================================================*/
    /**
    * @brief 初期化処理
    */
    // Preference内にメニューの追加.
    [PreferenceItem("CustomMenu")]
    // メニューから呼び出せるエディタの項目を追加.
    [MenuItem("CustomMenu/Create/Create Objects To Positions")]
    static void Init () 
    {
        // 専用のウィンドウを表示.
        EditorWindow.GetWindow<CreateObjectToPositionsEditor>(true, "Create Objects To Positions");
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ウィンドウ表示時に自動で呼ばれるメソッド。
    */ 
    void OnEnable () 
    {
        if (Selection.gameObjects.Length > 0)
        {
            m_parent = Selection.gameObjects[0];
        }
        // 座標リストの初期化.
        Vector3[] positions = new Vector3[m_create_num];
        // 座標リストの生成.
        m_position_list = new List<Vector3>(positions);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 選択内容の変更時に自動で呼ばれるメソッド。
    */
    void OnSelectionChange () 
    {
        if (Selection.gameObjects.Length > 0) 
        {
            m_prefab = Selection.gameObjects[0];
        }
        Repaint();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 増減した座標の個数をチェックし,リストを増減するメソッド。
    */
    void CheckPositionList ()
    {
        // 座標の増加.
        if (m_position_list.Count < m_create_num)
        {
            for (int i = 0; i < (m_create_num - m_position_list.Count); i++)
            {
                m_position_list.Add(new Vector3(0, 0, 0));
            }
        }
        // 座標の減少.
        else if (m_position_list.Count > m_create_num)
        {
            for (int i = 0; i < (m_position_list.Count - m_create_num); i++)
            {
                m_position_list.RemoveAt(m_position_list.Count-1);
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 座標の表示メソッド。
    */
    void PositionListView ()
    {
        // スクロールビューの表示を開始.
        m_scroll_position = EditorGUILayout.BeginScrollView(m_scroll_position, GUILayout.Height(100));
        // 座標の入力フィールド作成.
        for (int i = 0; i < m_position_list.Count; i++)
        {
            m_position_list[i] = EditorGUILayout.Vector3Field("Position : "+i, m_position_list[i]);
        }
        // スクロールビューの表示を終了.
        EditorGUILayout.EndScrollView();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 座標の初期化処理
    * @param bool 初期化するかどうかの判定
    */
    private void InitializePosition (bool t_init)
    {
        if (t_init)
        {
            for (int i = 0; i < m_position_list.Count; i++)
            {
                m_position_list[i] = Vector3.zero;
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief オブジェクトの生成処理
    * @param bool 生成するかどうかの判定
    */
    private void Create (bool t_create) 
    {
        if (t_create)
        {
            if (m_prefab == null)
            {
                return;
            }
     
            int count = 0;

            foreach (Vector3 pos in m_position_list)
            {
                GameObject obj = Instantiate(m_prefab, pos, Quaternion.identity) as GameObject;
                obj.name = m_prefab.name + count++;
                if (m_parent)
                {
                    obj.transform.parent = m_parent.transform;
                }
                // 新しくオブジェクトを生成する時のUndo操作を登録.
                Undo.RegisterCreatedObjectUndo(obj, "Create Objects To Positions");
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief GUI表示処理
    */
    void OnGUI () 
    {
        try 
        {
            // 親オブジェクトの入力フィールドの表示.
            m_parent = EditorGUILayout.ObjectField("Parent", m_parent, typeof(GameObject), true) as GameObject;
            // 生成するオブジェクトのプレハブの入力フィールドの表示.
            m_prefab = EditorGUILayout.ObjectField("Prefab", m_prefab, typeof(GameObject), true) as GameObject;
            
            // 座標の個数の入力フィールドの表示.
            m_create_num = EditorGUILayout.IntField("Number of position:", m_create_num);
            // Labelの表示.
            GUILayout.Label("Positions : ", EditorStyles.boldLabel);

            if (m_create_num >= 0)
            {
                // レイアウトイベント時に処理.
                if (Event.current.type == EventType.Layout)
                {
                    // 座標の増減チェック.
                    CheckPositionList();
                    // 座標の入力フィールドの表示.
                    PositionListView();
                }
                else 
                {
                    // 座標の入力フィールドの表示.
                    PositionListView(); 
                }
            }

            GUILayout.Label("", EditorStyles.boldLabel);
            // 座標の初期化.
            if (GUILayout.Button("Initialize Positions"))
            {
                // ポップアップウィンドウの作成.
                PopupWindow pop = EditorWindow.GetWindow<PopupWindow>(true, "Yes or No");
                // 初期化処理の代入.
                pop.callBack = InitializePosition;
                // テキストの代入.
                pop.Text = "Do you want to initialize the Positions ?";
            }
            // オブジェクトの生成.
            if (GUILayout.Button("Create"))
            {
                // ポップアップウィンドウの作成.
                PopupWindow pop = EditorWindow.GetWindow<PopupWindow>(true, "Yes or No");
                // 初期化処理の代入.
                pop.callBack = Create;
                // テキストの代入.
                pop.Text = "Do you want to create an object ?";
            }
        } 
        catch (System.FormatException) 
        {
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief インスペクタ更新処理
    */
    void OnInspectorUpdate ()
    {
        Repaint();
    }
    /*===============================================================*/
}
/*===============================================================*/