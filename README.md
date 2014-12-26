##【Unity】UnityGameObjectEditor

####概要
これはUnityのエディタウィンドウを拡張して作成したオブジェクトの制御をするエディタです。このプロジェクトに含まれているエディタは3種類あります。

#####CreateObjectToPositionsEditor
このエディタはオブジェクトを複数の座標に一気に生成することができます。親オブジェクトを指定してオブジェクトをまとめることも可能です。

このエディタはメニューの(CustomMenu/Create/Create Objects To Positions)で呼び出し、項目を設定することでオブジェクトの生成ができます。

設定するエディタの項目には

+ Parent : 生成するオブジェクトの親オブジェクトの設定
+ Prefab : 生成するオブジェクトの設定
+ Number of position : オブジェクトを生成する座標の個数の設定
+ Positions : 座標の設定
+ Initialize Positions : 座標の初期化
+ Create : オブジェクトの生成

があります。

上記項目を設定してCreateボタンを押すことでHierarchyViewにオブジェクトが生成されます。

#####PutObjectToPositionsEditor
このエディタは親オブジェクトの全ての子供オブジェクトの座標を調整することができます。勿論、親オブジェクトの座標を調整することもできます。

このエディタはメニューの(CustomMenu/Put/Put Objects To Positions)で呼び出し、項目を設定することでオブジェクトの座標の調整ができます。

設定するエディタの項目には

+ Parent : 調整したい子供オブジェクトの親オブジェクトの設定
+ Positions : 座標の設定
+ Initialize Positions : 座標の初期化

があります。

上記項目のPositionsの値を調整することで調整したいオブジェクトの座標が変動します。

#####PopupWindow
このエディタは簡易的に用意したポップアップウィンドウです。

####利用方法
Assets/Scripts/Editor内にスクリプトを置くことで利用できます。

####License
Mit license