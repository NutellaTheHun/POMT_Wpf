   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   orderFormGridStyle  �� 
TargetType&    System.Windows.Controls.DataGrid �� Property �� Value �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name � ColumnDefinitions � RowDefinitions	    SelectAllCommand
 �� RelativeSource �� AncestorType �� 
ResourceId �� TypeInTargetAssembly7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!    HeadersVisibilityConverter �� 	ConverterG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� Mode  �� ScrollableWidth! �� ViewportWidth," �� %ComputedHorizontalScrollBarVisibility# �� Triggers$ ]� 
Conditions% �� Property& �� Value�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      6W   %K
  &	        �� 5      "    6'   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � �� �#efefef6'   �� 5   
    � �� �	#FF688CAF6(   �� 5   
    � ��$ 1q�6,   �� 5   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5   
    � ��$ False��6:   �� 5	   
    �@  GridLinesVisibility$
 None=�60   �� 5
   
    �@  AutoGenerateColumns$ False��60   �� 5   
    �@  CanUserAddRows$ False��6+   �� 5   
    �@  CanUserDeleteRows$ False��6.   �� 5   
    �@  CanUserResizeRows$ False��6.   �� 5   
    �@  CanUserReorderColumns$ False��62   �� 5   
    �@  HeadersVisibility$ Column=�6.   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� # z��5      #��z��5      #��z��5      #��z��5      $
 True��5      ._�5      �� $ DG_ScrollViewer��6'   $|�false��6@   ��5      �� 5   "   " ��62   .R�5   &   �  5   *   �� 5   .   $
��Auto �6?   �� 5   .   $��* �6?   �� 5    .   $
��Auto �6?    5"   *   �� 5#   .   $
��Auto �6<   �� 5$   .   $��* �6<   �� 5%   .   $
��Auto �6<   .+�5'   *   �� #��Z	 5)   -   $|�false��5*   -   ��5(   -   �� *CellsPanelHorizontalOffset+
 �� "   ��5+   -   C� *�� $" DataGridSelectAllButtonStyle��"   +y�5-   -   �� *HeadersVisibility+# Z # Z 
 �� "     5.   *   $! PART_ColumnHeadersPresenter��5/   -   $��1��50   -   y�51   -   �� *HeadersVisibility+# Z # Z 
 �� "   �� 52   *   $! PART_ScrollContentPresenter��53   -   $��1��54   -   $��2��55   -   # z��56   -   �� 57   *   $ PART_VerticalScrollBar��58   -   $��1��59   -   $��2��5:   -   # z 5;   -   $ Vertical=�5<   -   # z 5=   -   #y�z 5>   -    5?   -   �� *VerticalOffset+$ OneWay=�
 �� $ TemplatedParent=�� 5@   *   $��2��6/   $��1��6<    5A   .   �� 5B   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+
 �� "   �� 5C   2   $��* �6C   .+�5E   .   �� $ PART_HorizontalScrollBar��5F   1   $��1��5G   1   # z  5H   1   $ 
Horizontal=�5I   1   # z! 5J   1   #y�z" 5K   1    5L   1   �� *HorizontalOffset+$ OneWay=�
 �� $ TemplatedParent=�5M   +   5N   '   5O   #   .��5Q      �� # z 6-   5R      5S      5T      5V      # 5W   
   ]� 5X      $ 5Y      �� 5Z      % �@��
IsGrouping$
& true��66   �� 5[      "% �@%�IsVirtualizingWhenGrouping$& false��6X   ./�5]      ��  � ��$ false��6B   5^      5`      5a      