using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
/*===============================================================*/
/**
* Hierarchy上のオブジェクトを選択して一気に削除するエディタ
* 2014年12月25日 Buravo
*/ 
public class DeleteObjectToSelectEditor : EditorWindow 
{

    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief Hierarchyに存在するゲームオブジェクトのリスト
    */
    private List<GameObject> m_object_list;
    /**
    * @brief オブジェクトの削除を判断するリスト
    */
    private List<bool> m_delete_list;
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
    [MenuItem("CustomMenu/GameObject/Delete Object To Select")]
    static void Init () 
    {
        // 専用のウィンドウを表示.
        EditorWindow.GetWindow<DeleteObjectToSelectEditor>(true, "Delete Object To Select");
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ウィンドウ表示時に自動で呼ばれるメソッド
    */ 
    void OnEnable () 
    {
        CheckHierarchy();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 選択内容の変更時に自動で呼ばれるメソッド
    */
    void OnSelectionChange () 
    {
        Repaint();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief Hierarchyのオブジェクトをチェックし,リストへ格納するメソッド
    */
    void CheckHierarchy ()
    {
    	// リストの初期化.
        m_object_list = new List<GameObject>();
        m_delete_list = new List<bool>();
        // HierarchyのGameObjectを持つ全オブジェクトを取得.
        foreach (GameObject obj in UnityEngine.GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.activeInHierarchy)
            {
            	m_object_list.Add(obj);
            	m_delete_list.Add(false);
            }
        }
        // 要素の反転.
        m_object_list.Reverse();
        m_delete_list.Reverse();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 削除できるオブジェクトの一覧を表示するメソッド
    */
    void DeleteObjectListView ()
    {
        // スクロールビューの表示を開始.
        m_scroll_position = EditorGUILayout.BeginScrollView(m_scroll_position, GUILayout.Height(100));
        // フィールド作成.
        for (int i = 0; i < m_object_list.Count; i++)
        {
        	m_delete_list[i] = EditorGUILayout.Toggle(m_object_list[i].name, m_delete_list[i]);
        }
        // スクロールビューの表示を終了.
        EditorGUILayout.EndScrollView();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 選択の初期化処理
    * @param bool 初期化するかどうかの判定
    */
    private void InitializeSelection (bool t_init) 
    {
        if (t_init)
        {
            for (int i = 0; i < m_delete_list.Count; i++)
            {
                m_delete_list[i] = false;
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief オブジェクトの削除処理
    * @param bool 削除するかどうかの判定
    */
    private void Delete (bool t_delete) 
    {
        if (t_delete)
        {
            for (int i = 0; i < m_object_list.Count; i++)
            {
                if (m_delete_list[i])
                {
                	// ゲームオブジェクトまたはコンポーネントを破棄.
                    Undo.DestroyObjectImmediate(m_object_list[i]);
                    // Editorのコード内で削除する場合に使用する削除関数.
                    Object.DestroyImmediate(m_object_list[i]);
                }
            }
            CheckHierarchy();
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
            // Labelの表示.
            GUILayout.Label("Delete Object : ", EditorStyles.boldLabel);

            // 削除できるオブジェクトの一覧を表示.
            DeleteObjectListView();
            
            // 水平に配置するGUIグループの作成を開始.
            EditorGUILayout.BeginHorizontal();
            // レイアウトグループ内に全体の幅に対して均一となるスペースを生成し挿入する.
            GUILayout.FlexibleSpace();
            // 垂直に配置するGUIグループの作成を開始.
            EditorGUILayout.BeginVertical();

            // スペースで間隔をとる.
            EditorGUILayout.Space();
            // 選択の初期化.
            if (GUILayout.Button("Initialize Selection", GUILayout.Width(150), GUILayout.Height(50)))
            {
                // ポップアップウィンドウの作成.
                PopupWindow pop = EditorWindow.GetWindow<PopupWindow>(true, "Do you want to initialize the Selection ?");
                // 初期化処理の代入.
                pop.callBack = InitializeSelection;
                // テキストの代入.
                pop.Text = "Do you want to initialize the Selection ?";
            }

            // スペースで間隔をとる.
            EditorGUILayout.Space();
            // オブジェクトの削除.
            if (GUILayout.Button("Delete", GUILayout.Width(150), GUILayout.Height(50)))
            {
                // ポップアップウィンドウの作成.
                PopupWindow pop = EditorWindow.GetWindow<PopupWindow>(true, "Do you want to delete an object ?");
                // 削除処理の代入.
                pop.callBack = Delete;
                // テキストの代入.
                pop.Text = "Do you want to delete an object ?";
            }

            // 垂直に配置するGUIグループの作成を終了.
            EditorGUILayout.EndVertical();
            // レイアウトグループ内に全体の幅に対して均一となるスペースを生成し挿入する.
            GUILayout.FlexibleSpace();
            // 水平に配置するGUIグループの作成を終了.
            EditorGUILayout.EndHorizontal();
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
