   M S B A M L   `   `   `  ���� C  ?POMT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:POMT_WPF.MVVM.View.ControlsPOMT_WPF.MVVM.View.Controls  %    POMT_WPF.MVVM.View.SettingsView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:POMT_WPF.MVVM.View.Controls     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .���    ColumbiaBlue#��[    � RowDefinitions  �� $	��100 ��� $
��auto ��� $
��auto �.+�� $��1��$��Center=� � ColumnDefinitions �� $
��auto ��� $
��auto �.+�� $��0��$��10q�.+���  �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Cutie Labels Filepath���� $	��225��$��12(� �� 
IsReadOnly$
 true��  GeneralTextBox#��[ ���� *CutieLabelsFilepath+ �� UpdateSourceTrigger$ PropertyChanged=���   GeneralSetButton#��[ ���� *SetCutieLabelFilePathCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Pie Labels Filepath���� $	��225��$��12(�$
 true��#��[ ���� *PieLabelsFilepath+$ PropertyChanged=��� #��[ ���� *SetPieLabelFilePathCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Environment Filepath���� $	��225��$��12(�$
 true��#��[ ���� *EnvironmentFilepath+$ PropertyChanged=��� #��[ ���� *SetEnvironmentFilePathCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Report Export Filepath���� $	��225��$��12(�$
 true��#��[ ���� *ReportExportFilepath+$ PropertyChanged=��� #��[ ���� * SetReportExportFilePathCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Backups Filepath���� $	��225��$��12(�$
 true��#��[ ���� *BackupFilepath+$ PropertyChanged=��� #��[ ���� *SetBackupFilePathCommand+� $��1��$��10q�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Select Pie Template���� $
 true��#��[ ���� *PieTemplate+$ PropertyChanged=��� #��[ ���� *SetPieTemplateCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Select PastryTemplate���� $
 true��#��[ ���� *PastryTemplate+$ PropertyChanged=��� #��[ ���� *SetPastryTemplateCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Label Printer���� $
 true��#��[ ���� *LabelPrinter+$ PropertyChanged=��� #��[ ���� *SetLabelPrinterCommand+�� $ 
Horizontal=�.+��� $ Vertical=�.+��� $��Standard Printer���� $
 true��#��[ ���� *StandardPrinter+$ PropertyChanged=��� #��[ ���� *SetStandardPrinterCommand+� $��2��$��10q�$��Center=�.+��� $ 
Horizontal=�.+��� $	��150��$��5q�$��Configure Labels��#��[ ���� *ConfigureLabelsCommand+�� $	��150��$��5q�$��Configure Templates��#��[ ���� *ConfigureTemplatesCommand+