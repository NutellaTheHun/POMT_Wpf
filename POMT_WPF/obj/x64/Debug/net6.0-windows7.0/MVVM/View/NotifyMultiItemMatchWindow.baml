   M S B A M L   `   `   `  ����C  ?POMT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POMT_WPF.MVVM.ViewPOMT_WPF.MVVM.View  5      3    -POMT_WPF.MVVM.View.NotifyMultiItemMatchWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:POMT_WPF.MVVM.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$   NotifyMultiItemMatchWindow��5      $	��400��5	      $	��300��5
       � AllowsTransparency$
 True��5      
������  � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      ��    GeneralWindowBorder#��[  6   ._�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5      � $��0��6   .+�5      �� $��Center=�6   ��69   �� *ItemContext+ �� StringFormat$+ %Multiple items matched for item: {o} ��5      � 5      $��1��6   .+�5      �� $	��320��5      $	��220��5        GeneralBorder#��[ 5      ._�5       �� -   5         ��Name$ multiItemListBox��5!      $	��300��5"      $	��200��5#      $��Center=�5$      ��5%      �� *MultiItemList+��5&      �� 5'      .R�5(   "   �� ��6,   �� *
ItemName+5)      5+      5-      5.      � 5/      $��2��6   .+�50      �� $��Center=�6    �� Orientation$ 
Horizontal=�6:   .+�51      �� $	��100��52      $��40��53      $��0,0,20,0q�54      $��New Item��56        secondaryButton#��[ 57      ��55      �� *
CreateItem+57      �� 58      $	��100��59      $��40��5:      $��0,0,20,0q�5;      $��Select Item��5>      #��[ 5?      ��5<      �� *
SelectItem+��5=      �� 	 �� ElementName$	 multiItemListBox��
 �� Path$
 SelectedItem�5?      5@      5A      5B      5C      5D      