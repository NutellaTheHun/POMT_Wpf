   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   dashboardDataGridStyle  �� 
TargetType&    System.Windows.Controls.DataGrid �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name � ColumnDefinitions � RowDefinitions	    SelectAllCommand
 �� RelativeSource �� AncestorType �� 
ResourceId �� TypeInTargetAssembly7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!    HeadersVisibilityConverter �� 	ConverterG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� Mode  �� ScrollableWidth! �� ViewportWidth," �� %ComputedHorizontalScrollBarVisibility# �� Triggers$ ]� 
Conditions% �� Property& �� Value�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      6W   %  &	        �� 5      "    6+   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � �� �#2225286'   �� 5   
    � ��$ 0q�6,   �� 5   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5   
    � ��$ False��6:   �� 5   
    �@  AutoGenerateColumns$ False��60   �� 5	   
    �@  HeadersVisibility$ Column=�6.   �� 5
   
    � ��$ 0,10,0,0q�6#   �� 5   
    �@  SelectionMode$ single=�6*   �� 5   
    �@  SelectionUnit$ FullRow=�6*   �� 5   
    �@  CanUserAddRows$ False��6+   �� 5   
    �@  CanUserDeleteRows$ False��6.   �� 5   
    �@  CanUserResizeRows$ False��6.   �� 5   
    �@  CanUserReorderColumns$ False��62   �� 5   
    � ��$ 13(�6%   �� 5   
    �@  ColumnWidth$ * 6(   �� 5   
    �@  GridLinesVisibility$
 None=�60   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� # z��5      #��z��5      #��z��5      #��z��5      $
 True��5      ._�5      �� $ DG_ScrollViewer��6'   $|�false��6@   ��5      �� 5   "   " ��62   .R�5    &   �  5!   *   �� 5"   .   $
��Auto �6?   �� 5#   .   $��* �6?   �� 5$   .   $
��Auto �6?    5&   *   �� 5'   .   $
��Auto �6<   �� 5(   .   $��* �6<   �� 5)   .   $
��Auto �6<   .+�5+   *   �� #��Z	 5-   -   $|�false��5.   -   ��5,   -   �� *CellsPanelHorizontalOffset+
 �� "   ��5/   -   C� *�� $+ %dashboardDataGridSelectAllButtonStyle��"   +y�51   -   �� *HeadersVisibility+# Z # Z 
 �� "     52   *   $! PART_ColumnHeadersPresenter��53   -   $��1��54   -   y�55   -   �� *HeadersVisibility+# Z # Z 
 �� "   �� 56   *   $! PART_ScrollContentPresenter��57   -   $��1��58   -   $��2��59   -   # z��5:   -   �� 5;   *   $ PART_VerticalScrollBar��5<   -   $��1��5=   -   $��2��5>   -   # z 5?   -   $ Vertical=�5@   -   # z 5A   -   #y�z 5B   -    5C   -   �� *VerticalOffset+$ OneWay=�
 �� $ TemplatedParent=�� 5D   *   $��2��6/   $��1��6<    5E   .   �� 5F   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+
 �� "   �� 5G   2   $��* �6C   .+�5I   .   �� $ PART_HorizontalScrollBar��5J   1   $��1��5K   1   # z  5L   1   $ 
Horizontal=�5M   1   # z! 5N   1   #y�z" 5O   1    5P   1   �� *HorizontalOffset+$ OneWay=�
 �� $ TemplatedParent=�5Q   +   5R   '   5S   #   .��5U      �� # z 6-   5V      5W      5X      5Z      # 5[   
   ]� 5\      $ 5]      �� 5^      % �@��
IsGrouping$
& true��66   �� 5_      "% �@%�IsVirtualizingWhenGrouping$& false��6X   ./�5a      ��  � ��$ false��6B   5b      5d      5e      