   M S B A M L   `   `   `  ����C  ?POMT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:POMT_WPF.MVVM.View.ControlsPOMT_WPF.MVVM.View.Controls  5      %    POMT_WPF.MVVM.View.SettingsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:POMT_WPF.MVVM.View.Controls     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �    ColumbiaBlue#��[  6     � RowDefinitions  5   
   �� 5      $	��100 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��1��6   $��Center=�6    � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      � $��0��6   $��10q�6#   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $ Vertical=�6%   .+�5      �� $��Cutie Labels Filepath��6$   �� 5      $	��225��5   !   $��12(�5    !    �� 
IsReadOnly$
 true��5!   !     GeneralTextBox#��[ 5"   !   ��5#   !   �� *CutieLabelsFilepath+ �� UpdateSourceTrigger$ PropertyChanged=�5%      �� 5&        GeneralSetButton#��[ 6R   ��6!   �� *SetCutieLabelFilePathCommand+6R   5'      �� 5(      $ 
Horizontal=�6!   .+�5)      �� $ Vertical=�6%   .+�5*      �� $��Pie Labels Filepath��6$   �� 5+      $	��225��5,   !   $��12(�5-   !   $
 true��5.   !   #��[ 5/   !   ��50   !   �� *PieLabelsFilepath+$ PropertyChanged=�51      �� 52      #��[ 6P   ��6!   �� *SetPieLabelFilePathCommand+6P   53      �� 54      $ 
Horizontal=�6!   .+�55      �� $ Vertical=�6%   .+�56      �� $��Environment Filepath��6$   �� 57      $	��225��58   !   $��12(�59   !   $
 true��5:   !   #��[ 5;   !   ��5<   !   �� *EnvironmentFilepath+$ PropertyChanged=�5=      �� 5>      #��[ 6S   ��6!   �� *SetEnvironmentFilePathCommand+6S   5?      �� 5@      $ 
Horizontal=�6!   .+�5A      �� $ Vertical=�6%   .+�5B      �� $��Report Export Filepath��6$   �� 5C      $	��225��5D   !   $��12(�5E   !   $
 true��5F   !   #��[ 5G   !   ��5H   !   �� *ReportExportFilepath+$ PropertyChanged=�5I      �� 5J      #��[ 6T   ��6!   �� * SetReportExportFilePathCommand+6T   5K      �� 5L      $ 
Horizontal=�6!   .+�5M      �� $ Vertical=�6%   .+�5N      �� $��Backups Filepath��6$   �� 5O      $	��225��5P   !   $��12(�5Q   !   $
 true��5R   !   #��[ 5S   !   ��5T   !   �� *BackupFilepath+$ PropertyChanged=�5U      �� 5V      #��[ 6N   ��6!   �� *SetBackupFilePathCommand+6N   5W      5X      5Y      � 5Z      $��1��6   $��10q�6#   .+�5[      �� $ Vertical=�6   .+�5\      �� $ 
Horizontal=�6!   .+�5]      �� $ Vertical=�6%   .+�5^      �� $��Select Pie Template��6$   �� 5`      $
 true��5a   !   #��[ 5b   !   ��5c   !   �� *PieTemplate+$ PropertyChanged=�5e      �� 5f      #��[ 6K   ��6!   �� *SetPieTemplateCommand+6K   5g      �� 5h      $ 
Horizontal=�6!   .+�5i      �� $ Vertical=�6%   .+�5j      �� $��Select PastryTemplate��6$   �� 5k      $
 true��5l   !   #��[ 5m   !   ��5n   !   �� *PastryTemplate+$ PropertyChanged=�5o      �� 5p      #��[ 6N   ��6!   �� *SetPastryTemplateCommand+6N   5q      �� 5r      $ 
Horizontal=�6!   .+�5s      �� $ Vertical=�6%   .+�5t      �� $��Label Printer��6$   �� 5u      $
 true��5v   !   #��[ 5w   !   ��5x   !   �� *LabelPrinter+$ PropertyChanged=�5y      �� 5z      #��[ 6L   ��6!   �� *SetLabelPrinterCommand+6L   5{      �� 5|      $ 
Horizontal=�6!   .+�5}      �� $ Vertical=�6%   .+�5~      �� $��Standard Printer��6$   �� 5      $
 true��5�   !   #��[ 5�   !   ��5�   !   �� *StandardPrinter+$ PropertyChanged=�5�      �� 5�      #��[ 6O   ��6!   �� *SetStandardPrinterCommand+6O   5�      5�      5�      5�      � 5�   
   $��2��5�      $��10q�5�      $��Center=�5�      .+�5�      �� $ 
Horizontal=�6   .+�5�      �� $	��150��5�      $��5q�5�      $��Configure Labels��5�      #��[ 5�      ��5�      �� *ConfigureLabelsCommand+5�      �� 5�      $	��150��5�      $��5q�5�      $��Configure Templates��5�      #��[ 5�      ��5�      �� *ConfigureTemplatesCommand+5�      5�      5�      5�      5�      