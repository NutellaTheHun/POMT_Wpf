   M S B A M L   `   `   `  ���� C  ?POMT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:POMT_WPF.ConvertersPOMT_WPF.Converters  7 clr-namespace:POMT_WPF.MVVM.ViewPOMT_WPF.MVVM.View  9!clr-namespace:POMT_WPF.ValidationPOMT_WPF.Validation  (    "POMT_WPF.MVVM.View.CatalogItemView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^ ZMahApps.Metro.IconPacks, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag	 cMahApps.Metro.IconPacks.Codicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah
 dMahApps.Metro.IconPacks.Coolicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.PicolIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PixelartIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RadixIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.RemixIcon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai  eMahApps.Metro.IconPacks.RPGAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj! fMahApps.Metro.IconPacks.SimpleIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag" cMahApps.Metro.IconPacks.Typicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af# bMahApps.Metro.IconPacks.Unicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj$ fMahApps.Metro.IconPacks.VaadinIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak% gMahApps.Metro.IconPacks.WeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah& dMahApps.Metro.IconPacks.Zondicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137axIcon-http://metro.mahapps.com/winfx/xaml/iconpacks!    	 
                        ! " # $ % & oconvert!clr-namespace:POMT_WPF.Converters!    	 
                        ! " # $ % & zd2http://schemas.microsoft.com/expression/blend/2008!    	 
                        ! " # $ % & llocal clr-namespace:POMT_WPF.MVVM.View!    	 
                        ! " # $ % & �mc;http://schemas.openxmlformats.org/markup-compatibility/2006!    	 
                        ! " # $ % & iv!clr-namespace:POMT_WPF.Validation!    	 
                        ! " # $ % &   � 	Resources     	BoolToVis-   'POMT_WPF.Converters.InvertBoolConverter  
InvertBool��%   &	        &	      ��   .���   ColumbiaBlue#��[  � RowDefinitions �� $	��100 ��� $
��auto ��� $
��auto �.+�� $��0��.+��� $��10q�$
��Left=�$��Bottom=�  GeneralBackButton#��[ ���� *BackCatalogItem+.��.  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ ChevronLeft=��� $��60��$��20��$��
0,0,200,10q�$��Right=�$��Bottom=�  GeneralBorder#��[ ._�d� $
��Edit��  orderFormEditButton#��[  d� 	IsChecked �� *IsEdit+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�� $��1��.+��� $	��620��#��[ ._�� $��Center=�$��Center=� � ColumnDefinitions �� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0��$��30q�.+��� $��Center=�$��Center=� �� Orientation$ Vertical=�.+��� $ Vertical=�.+��� $��	Item Name���� -    ��Name$ ItemNameErrBdr��  OrderFormErrorBorder#��[ ._�� $��2q�.+�� .+��� -   	 ��Name$	 ItemNameComboBox��$
��Left=�  ItemNameComboBox#��[ 
 �� Text
 ��  �� ElementName$ ItemNameTextBox�� �� Path$
 Text��� -    ��Name$ ItemNameTextBox��$
��Left=�  ItemNameTextBox#��[  �� 
IsReadOnly �� *IsEdit+ �� 	Converter# [ ���� *
ItemName+$ PropertyChanged=�$ TwoWay=��� $ Vertical=�.+��� $��Alternative Names���� $ 
Horizontal=�.+��� -    ��Name$ AltNameListBox��$	��225��$	��100��$��14(� J� IsHitTestVisible �� *IsEdit+���� *NaturalNames+$ PropertyChanged=��� $ Vertical=�.+��� $��30��$��30��$��5,0,5,5q�$��Right=�$	��Top=� 	 secondaryButton#��[	 ���� *
AddAltName+ �� *IsEdit+.��  $
 Plus=��� $��30��$��30��$��5,0,5,5q�$��Right=�$	��Top=� 
 ChiliRed#��[
 #��[	 ���� *RemoveAltName+���� $ AltNameListBox��$ SelectedItem� �� *IsEdit+.��  $ Minus=��� $ Vertical=�.+��� $��
Attributes���� -   $ ItemSizesErrBdr��#��[ ._�� $��2q�.+��� #��[ ._�� $��0,3,0,3q�.+��� -   $ 
Horizontal=� �� *IsEdit+.+��� $��	20,0,20,0q�$ Vertical=�.+��� $��Small��$��16(�  	maybeDark#��[  �� *	IsSmall+$ PropertyChanged=�$ TwoWay=��� $��	0,10,0,10q�$��Medium��$��16(�#��[  �� *
IsMedium+$ PropertyChanged=�$ TwoWay=��� $��Large��$��16(�#��[  �� *	IsLarge+$ PropertyChanged=�$ TwoWay=��� $ Vertical=�.+��� $��Regular��$��16(�#��[  �� *	IsRegular+$ PropertyChanged=�$ TwoWay=��� $��0,10,0,5q�$��Cutie��$��16(�#��[  �� *	IsCutie+$ PropertyChanged=�$ TwoWay=��� $��0,5,0,10q�$
��POTM��$��16(�#��[  �� *IsPOTM+$ PropertyChanged=�$ TwoWay=��� $��Parbake��$��16(�#��[  �� *	IsParbake+$ PropertyChanged=�$ TwoWay=�� $��1��.+��� $��0,30,0,0q�$��Center=�$	��Top=�$ Vertical=�.+��� $ Vertical=�.+��� $ Vertical=�.+��� $��Category���� -   $ CategoryNameErrBdr��#��[ ._�� $��2q�.+��� -   $	��150��$��30��$
��Left=�$
��Left=�$��Center=�  TextFont#��[  �� *IsEdit+���� *CategoryNames+ �� SelectedItem �� $ CategoryName�$ PropertyChanged=�$ TwoWay=��� $ Vertical=�.+��� $��	Vegan Pie���� $ 
Horizontal=�.+��� $
��Left=�$
 True��  GeneralTextBox#��[ ���� *VeganMappedItemName+$ PropertyChanged=�$ TwoWay=��� $��30��$��30��$��5,0,5,5q�$��Right=�$��Center=�#��[	 ���� *SetVeganPie+ �� *IsEdit+.��  $ ChartPie=��� $ Vertical=�.+��� $��Take N Bake���� $ 
Horizontal=�.+��� $
��Left=�$
 True��#��[ ���� *TakeBakeMappedItemName+$ PropertyChanged=�$ TwoWay=��� $��30��$��30��$��5,0,5,5q�$��Right=�$��Center=�#��[	 ���� *SetTakeBakePie+ �� *IsEdit+.��  $ ChartPie=��� $ Vertical=�.+��� $��Vegan Take N Bake���� $ 
Horizontal=�.+��� $
��Left=�$
 True��#��[ ���� *VeganTakeBakeMappedItemName+$ PropertyChanged=�$ TwoWay=��� $��30��$��30��$��5,0,5,5q�$��Right=�$��Center=�#��[	 ���� *SetVeganTakeBakePie+ �� *IsEdit+.��  $ ChartPie=��� $ Vertical=�.+��� $��Standard Label File���� $ 
Horizontal=�.+��� $	��250��$
��Left=�$��11(�$
 True��#��[ ���� *StandardLabelFilePath+$ PropertyChanged=�$ TwoWay=��� $��30��$��30��$��5,0,5,5q�$��Right=�$��Center=�#��[	 ���� *SetStandardLblFile+ �� *IsEdit+.��  $
 File=��� $��0,0,0,5q�$ Vertical=�.+��� $��Cutie Label File���� $ 
Horizontal=�.+��� $	��250��$
��Left=�$��11(�$
 True��#��[ ���� *CutieLabelFilePath+$ PropertyChanged=�$ TwoWay=��� $��30��$��30��$��5,0,5,5q�$��Right=�$��Center=�#��[	 ���� *SetCutieLblFile+ �� *IsEdit+.��  $
 File=�� $��2��.+��� -	   $��SaveOrderButton��$��10q�$��Center=�$
��Save��#��[	 ���� *
SaveItem+.  (MahApps.Metro.IconPacks.PackIconBoxIcons  -
     Name$ SaveCheckMark��  	CheckMark#��[ �� $��	0,0,200,0q�$��Right=�#��[
 $��Delete��#��[	 ���� *
DeleteItem+y��� *	CanDelete+# [  