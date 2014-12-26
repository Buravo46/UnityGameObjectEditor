using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
/*===============================================================*/
/**
* 設定した親オブジェクトの子供オブジェクトを全て取得し,取得した子供オブジェクトの座標を調整するエディタ
* 2014年12月25日 Buravo
*/ 
public class PutObjectToPositionsEditor : EditorWindow 
{

    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief 親ゲームオブジェクト
    */
    private GameObject m_parent;
    /**
    * @brief 子供のオブジェクトリスト
    */
    private List<GameObject> m_child_list;
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
    // メニューから呼び出せるエディタの項目を追加.
    [MenuItem("CustomMenu/Put/Put Objects To Positions")]
    static void Init () 
    {
        // 専用のウィンドウを表示.
        EditorWindow.GetWindow<PutObjectToPositionsEditor>(true, "Put Objects To Positions");
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
        // リストの初期化.
        m_child_list = new List<GameObject>();
        // もしも親オブジェクトが存在していれば.
        if (m_parent)
        {
            // 子供のTransform数を取得し、子供オブジェクトをリストに格納する.
            for (int i = 0; i < m_parent.transform.childCount; i++) 
            {
                m_child_list.Add(m_parent.transform.GetChild(i).gameObject);
            }
        }
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
            m_parent = Selection.gameObjects[0];
        }
        // リストの初期化.
        m_child_list = new List<GameObject>();
        // もしも親オブジェクトが存在していれば.
        if (m_parent)
        {
            // 子供のTransform数を取得し、子供オブジェクトをリストに格納する.
            for (int i = 0; i < m_parent.transform.childCount; i++) 
            {
                m_child_list.Add(m_parent.transform.GetChild(i).gameObject);
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief エディタからインスペクターの値を変更した際に呼ばれるメソッド。
    */
    void PositionListView ()
    {
        // スクロールビューの表示を開始.
        m_scroll_position = EditorGUILayout.BeginScrollView(m_scroll_position, GUILayout.Height(100));
        // 親オブジェクトの座標の入力フィールド表示.
        m_parent.transform.position = EditorGUILayout.Vector3Field("Parent : "+m_parent.name, m_parent.transform.position);
        // 全ての子供オブジェクトの座標の入力フィールド表示.
        for (int i = 0; i < m_child_list.Count; i++)
        {
            m_child_list[i].transform.position = EditorGUILayout.Vector3Field("Chiled : "+m_child_list[i].name, m_child_list[i].transform.position);
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
            // 親オブジェクトの座標を初期化.
            m_parent.transform.position = Vector3.zero;
            // 全ての子供オブジェクトの座標を初期化.
            for (int i = 0; i < m_child_list.Count; i++)
            {
                m_child_list[i].transform.position = Vector3.zero;
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
            
            // Labelの表示.
            GUILayout.Label("Positions : ", EditorStyles.boldLabel);

            // もしも親オブジェクトが存在していれば.
            if (m_parent)
            {
                EditorGUI.BeginChangeCheck();
                // 座標の入力フィールドの表示.
                PositionListView();
                // インスペクタで要素が変更されたかどうかを確認.
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (GameObject child in m_child_list)
                    {
                        // １つのプロパティを修正.
                        Undo.RecordObject(child.transform, "Put Objects To Positions");
                    }
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