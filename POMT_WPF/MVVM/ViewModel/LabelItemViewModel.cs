using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Windows.Forms;

namespace POMT_WPF.MVVM.ViewModel
{
    class LabelItemViewModel : ViewModelBase
    {
        private CatalogItemPetsi _item;
        private LabelItemWindow _view;

        public string ItemName
        {
            get { return _item.ItemName; }
            set 
            {
                if (_item.ItemName != value)
                {
                    _item.ItemName = value;
                    OnPropertyChanged(nameof(ItemName)); 
                } 
            }
        }
        public string PieFile
        {
            get { return _item.StandardLabelFilePath; }
            set
            {
                if (_item.StandardLabelFilePath != value)
                {
                    _item.StandardLabelFilePath = value;
                    OnPropertyChanged(nameof(PieFile));
                }
            }
        }
        public string CutieFile
        {
            get { return _item.CutieLabelFilePath; }
            set
            {
                if (_item.CutieLabelFilePath != value)
                { 
                    _item.CutieLabelFilePath = value;
                    OnPropertyChanged(nameof(CutieFile));
                }
            }
        }

        public RelayCommand SetPieLabel {  get; set; }
        public RelayCommand SetCutieLabel {  get; set; }
        public RelayCommand CleartPieLabel {  get; set; }
        public RelayCommand ClearCutieLabel {  get; set; }
        public RelayCommand Done {  get; set; }
        public RelayCommand Cancel {  get; set; }
        public LabelItemViewModel(CatalogItemPetsi? item, LabelItemWindow view) 
        {
            _item = new CatalogItemPetsi(item);
            _view = view;

            SetPieLabel = new RelayCommand(o => { } );
            SetCutieLabel = new RelayCommand(o => { } );
            CleartPieLabel = new RelayCommand(o => { } );
            ClearCutieLabel = new RelayCommand(o => { } );
            Done = new RelayCommand(o => { } );
            Cancel = new RelayCommand(o => { } );
        }

        private void SetPieCommand()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Pie";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    PieFile = fileDialog.FileName;
                }
            }
        }
        private void SetCutieCommand()
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_FP);
            if (labelsFilepath != null || labelsFilepath != "")
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = labelsFilepath + "\\Cuties";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CutieFile = fileDialog.FileName;
                }
            }
        }
        private void ClearPieCommand()
        {
            PieFile = null;
        }
        private void ClearCutieCommand()
        {
            CutieFile = null;
        }
        private void DoneCommand() 
        {
            //Update Item
            _view.CloseWin();
        }
        private void CancelCommand() { _view.CloseWin(); }
    }
}
