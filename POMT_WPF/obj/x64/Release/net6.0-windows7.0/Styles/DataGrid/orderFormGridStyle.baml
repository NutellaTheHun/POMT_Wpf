   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   orderFormGridStyle  �� 
TargetType&    System.Windows.Controls.DataGrid �� Property �� Value �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name � ColumnDefinitions � RowDefinitions	    SelectAllCommand
 �� RelativeSource �� AncestorType �� 
ResourceId �� TypeInTargetAssembly7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!    HeadersVisibilityConverter �� 	ConverterG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� Mode  �� ScrollableWidth! �� ViewportWidth," �� %ComputedHorizontalScrollBarVisibility# �� Triggers$ ]� 
Conditions% �� Property& �� Value�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %�  &	        �� "    .���  � ��
 ���� ��  � �� �#efefef��  � �� �	#FF688CAF��  � ��$ 1q���   �@  RowDetailsVisibilityMode$ VisibleWhenSelected=���  � ��$ False����  �@  GridLinesVisibility$
 None=���  �@  AutoGenerateColumns$ False����  �@  CanUserAddRows$ False����  �@  CanUserDeleteRows$ False����  �@  CanUserResizeRows$ False����  �@  CanUserReorderColumns$ False����  �@  HeadersVisibility$ Column=���  � �� �� "   .R��� # z��#��z��#��z��#��z��$
 True��._��� $ DG_ScrollViewer��$|�false������ " ��.R��  �� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� #��Z	 $|�false������ *CellsPanelHorizontalOffset+
 �� "   ��C� *�� $" DataGridSelectAllButtonStyle��"   +y��� *HeadersVisibility+# Z # Z 
 �� "     $! PART_ColumnHeadersPresenter��$��1��y��� *HeadersVisibility+# Z # Z 
 �� "   �� $! PART_ScrollContentPresenter��$��1��$��2��# z���� $ PART_VerticalScrollBar��$��1��$��2��# z $ Vertical=�# z #y�z  �� *VerticalOffset+$ OneWay=�
 �� $ TemplatedParent=�� $��2��$��1�� �� ���� **(NonFrozenColumnsViewportHorizontalOffset+
 �� "   �� $��* �.+��� $ PART_HorizontalScrollBar��$��1��# z  $ 
Horizontal=�# z! #y�z"  �� *HorizontalOffset+$ OneWay=�
 �� $ TemplatedParent=�.���� # z # ]� $ �� % �@��
IsGrouping$
& true���� "% �@%�IsVirtualizingWhenGrouping$& false��./���  � ��$ false��