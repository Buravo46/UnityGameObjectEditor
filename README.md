##【Unity】UnityGameObjectEditor

####概要
これはUnityのエディタウィンドウを拡張して作成したオブジェクトの制御をするエディタです。このプロジェクトに含まれているエディタは6種類あります。

#####CreateObjectToPositionEditor
このエディタはオブジェクトを複数の座標に一気に生成することができます。親オブジェクトを指定してオブジェクトをまとめることも可能です。

このエディタはメニューの(CustomMenu/GameObject/Create Object To Position)で呼び出し、項目を設定することでオブジェクトの生成ができます。

設定するエディタの項目には

+ Parent : 生成するオブジェクトの親オブジェクトの設定
+ Prefab : 生成するオブジェクトの設定
+ Number of position : オブジェクトを生成する座標の個数の設定
+ Positions : 座標の設定
+ Initialize Positions : 座標の初期化
+ Create : オブジェクトの生成

があります。

上記項目を設定してCreateボタンを押すことでHierarchyViewにオブジェクトが生成されます。

#####MoveObjectToPositionEditor
このエディタは親オブジェクトと子供オブジェクトの座標を調整することができます。

このエディタはメニューの(CustomMenu/GameObject/Move Object To Position)で呼び出し、項目を設定することでオブジェクトの座標の調整ができます。

設定するエディタの項目には

+ Parent : 調整したい子供オブジェクトの親オブジェクトの設定
+ Positions : 座標の設定
+ Initialize Positions : 座標の初期化

があります。

上記項目のPositionsの値を調整することで調整したいオブジェクトの座標が変動します。

#####RotateObjectToRotationEditor
このエディタは親オブジェクトと子供オブジェクトの角度をオイラー角で調整することができます。

このエディタはメニューの(CustomMenu/GameObject/Rotate Object To Rotation)で呼び出し、項目を設定することでオブジェクトの座標の調整ができます。

設定するエディタの項目には

+ Parent : 調整したい子供オブジェクトの親オブジェクトの設定
+ Rotations : 角度の設定
+ Initialize Rotations : 角度の初期化

があります。

上記項目のRotationsの値を調整することで調整したいオブジェクトの角度がオイラー角として変動します。

#####ScaleObjectToSizeEditor
このエディタは親オブジェクトと子供オブジェクトのスケールを調整することができます。

このエディタはメニューの(CustomMenu/GameObject/Scale Object To Size)で呼び出し、項目を設定することでオブジェクトの座標の調整ができます。

設定するエディタの項目には

+ Parent : 調整したい子供オブジェクトの親オブジェクトの設定
+ Sizes : スケールの設定
+ Initialize Sizes : スケールの初期化

があります。

上記項目のSizesの値を調整することで調整したいオブジェクトのスケールが変動します。

#####DeleteObjectToSelectEditor
このエディタはHierarchy上のオブジェクトを選択して一気に削除することができます。

このエディタはメニューの(CustomMenu/GameObject/Delete Object To Select)で呼び出し、項目を設定することでオブジェクトの座標の調整ができます。

設定するエディタの項目には

+ Delete Object : 削除するオブジェクトの選択
+ Initialize Selection : 選択の初期化
+ Delete : 選択したオブジェクトの削除
があります。

Delete ObjectにチェックしてDeleteボタンを押すことで一気にオブジェクトを削除することができます。

#####PopupWindow
このエディタは簡易的に用意したポップアップウィンドウです。

####利用方法
Assets/Scripts/Editor内にスクリプトを置くことで利用できます。

####License
Mit license