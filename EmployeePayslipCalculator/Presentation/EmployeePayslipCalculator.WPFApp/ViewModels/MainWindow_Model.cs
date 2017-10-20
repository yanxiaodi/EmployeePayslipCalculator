using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using EmployeePayslipCalculator.Models;
using EmployeePayslipCalculator.Service;

namespace EmployeePayslipCalculator.WPFApp.ViewModels
{

    public class MainWindow_Model : ViewModelBase<MainWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public MainWindow_Model()
        {
            if (IsInDesignMode)
            {
                Title = "Title is a little different in Design mode";
            }
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Excel Files (*.xlsx,*.xls)|*.xlsx;*.xls";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = false;
            this.SetMonths();
        }

        private void SetMonths()
        {
            this.Months = new Dictionary<string, int>();
            this.Months.Add("January", 1);
            this.Months.Add("February ", 2);
            this.Months.Add("March", 3);
            this.Months.Add("April", 4);
            this.Months.Add("May", 5);
            this.Months.Add("June", 6);
            this.Months.Add("July", 7);
            this.Months.Add("August", 8);
            this.Months.Add("September", 9);
            this.Months.Add("October", 10);
            this.Months.Add("November", 11);
            this.Months.Add("December", 12);
            this.SelectMonth = 1;
        }



        //propvm tab tab string tab Title
        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<String> _TitleDefaultValueFactory = () => "Welcome to Employee Salary Calculator";
        #endregion



        #region properties

        private System.Windows.Forms.OpenFileDialog openFileDialog;



        public Dictionary<string, int> Months
        {
            get { return _MonthsLocator(this).Value; }
            set { _MonthsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Dictionary<string, int> Months Setup        
        protected Property<Dictionary<string, int>> _Months = new Property<Dictionary<string, int>> { LocatorFunc = _MonthsLocator };
        static Func<BindableBase, ValueContainer<Dictionary<string, int>>> _MonthsLocator = RegisterContainerLocator<Dictionary<string, int>>("Months", model => model.Initialize("Months", ref model._Months, ref _MonthsLocator, _MonthsDefaultValueFactory));
        static Func<Dictionary<string, int>> _MonthsDefaultValueFactory = () => default(Dictionary<string, int>);
        #endregion



        public int SelectMonth
        {
            get { return _SelectMonthLocator(this).Value; }
            set { _SelectMonthLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int SelectMonth Setup        
        protected Property<int> _SelectMonth = new Property<int> { LocatorFunc = _SelectMonthLocator };
        static Func<BindableBase, ValueContainer<int>> _SelectMonthLocator = RegisterContainerLocator<int>("SelectMonth", model => model.Initialize("SelectMonth", ref model._SelectMonth, ref _SelectMonthLocator, _SelectMonthDefaultValueFactory));
        static Func<int> _SelectMonthDefaultValueFactory = () => default(int);
        #endregion



        public string SourceDataFilePath
        {
            get { return _SourceDataFilePathLocator(this).Value; }
            set { _SourceDataFilePathLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string SourceDataFilePath Setup        
        protected Property<string> _SourceDataFilePath = new Property<string> { LocatorFunc = _SourceDataFilePathLocator };
        static Func<BindableBase, ValueContainer<string>> _SourceDataFilePathLocator = RegisterContainerLocator<string>("SourceDataFilePath", model => model.Initialize("SourceDataFilePath", ref model._SourceDataFilePath, ref _SourceDataFilePathLocator, _SourceDataFilePathDefaultValueFactory));
        static Func<string> _SourceDataFilePathDefaultValueFactory = () => default(string);
        #endregion



        public string ResultOutputPath
        {
            get { return _ResultOutputPathLocator(this).Value; }
            set { _ResultOutputPathLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string ResultOutputPath Setup        
        protected Property<string> _ResultOutputPath = new Property<string> { LocatorFunc = _ResultOutputPathLocator };
        static Func<BindableBase, ValueContainer<string>> _ResultOutputPathLocator = RegisterContainerLocator<string>("ResultOutputPath", model => model.Initialize("ResultOutputPath", ref model._ResultOutputPath, ref _ResultOutputPathLocator, _ResultOutputPathDefaultValueFactory));
        static Func<string> _ResultOutputPathDefaultValueFactory = () => default(string);
        #endregion

        #endregion


        public CommandModel<ReactiveCommand, String> CommandSelectSourceDataFile
        {
            get { return _CommandSelectSourceDataFileLocator(this).Value; }
            set { _CommandSelectSourceDataFileLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSelectSourceDataFile Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSelectSourceDataFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSelectSourceDataFileLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSelectSourceDataFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSelectSourceDataFile", model => model.Initialize("CommandSelectSourceDataFile", ref model._CommandSelectSourceDataFile, ref _CommandSelectSourceDataFileLocator, _CommandSelectSourceDataFileDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSelectSourceDataFileDefaultValueFactory =
            model =>
            {
                var state = "CommandSelectSourceDataFile";           // Command state  
                var commandId = "CommandSelectSourceDataFile";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //Todo: Add SelectSourceDataFile logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            if (vm.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                vm.SourceDataFilePath = vm.openFileDialog.FileName;
                            }
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion



        public CommandModel<ReactiveCommand, String> CommandSelectResultOutputPath
        {
            get { return _CommandSelectResultOutputPathLocator(this).Value; }
            set { _CommandSelectResultOutputPathLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSelectResultOutputPath Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSelectResultOutputPath = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSelectResultOutputPathLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSelectResultOutputPathLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSelectResultOutputPath", model => model.Initialize("CommandSelectResultOutputPath", ref model._CommandSelectResultOutputPath, ref _CommandSelectResultOutputPathLocator, _CommandSelectResultOutputPathDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSelectResultOutputPathDefaultValueFactory =
            model =>
            {
                var state = "CommandSelectResultOutputPath";           // Command state  
                var commandId = "CommandSelectResultOutputPath";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //Todo: Add SelectResultOutputPath logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            if (vm.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                vm.ResultOutputPath = Path.GetDirectoryName(vm.openFileDialog.FileName);
                            }
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion


        public CommandModel<ReactiveCommand, String> CommandGenerateResultFile
        {
            get { return _CommandGenerateResultFileLocator(this).Value; }
            set { _CommandGenerateResultFileLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGenerateResultFile Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandGenerateResultFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGenerateResultFileLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGenerateResultFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGenerateResultFile", model => model.Initialize("CommandGenerateResultFile", ref model._CommandGenerateResultFile, ref _CommandGenerateResultFileLocator, _CommandGenerateResultFileDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGenerateResultFileDefaultValueFactory =
            model =>
            {
                var state = "CommandGenerateResultFile";           // Command state  
                var commandId = "CommandGenerateResultFile";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //Todo: Add GenerateResultFile logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            if (string.IsNullOrEmpty(vm.SourceDataFilePath) || string.IsNullOrEmpty(vm.ResultOutputPath))
                            {
                                MessageBox.Show("Please select the source data file and the output path", "Warning！", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            try
                            {
                                List<EmployeeInfo> list = vm.GetEmployeeList();
                                List<PayslipInfo> result = MVVMSidekick.Services.ServiceLocator.Instance.Resolve<PayslipCalculatorService>().Calculate(list, vm.SelectMonth);
                                vm.GenerateResult(result);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Error occured!", "Error！", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion



        public CommandModel<ReactiveCommand, String> CommandGenerateResultFileByWebApi
        {
            get { return _CommandGenerateResultFileByWebApiLocator(this).Value; }
            set { _CommandGenerateResultFileByWebApiLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGenerateResultFileByWebApi Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandGenerateResultFileByWebApi = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGenerateResultFileByWebApiLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGenerateResultFileByWebApiLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGenerateResultFileByWebApi", model => model.Initialize("CommandGenerateResultFileByWebApi", ref model._CommandGenerateResultFileByWebApi, ref _CommandGenerateResultFileByWebApiLocator, _CommandGenerateResultFileByWebApiDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGenerateResultFileByWebApiDefaultValueFactory =
            model =>
            {
                var state = "CommandGenerateResultFileByWebApi";           // Command state  
                var commandId = "CommandGenerateResultFileByWebApi";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //Todo: Add GenerateResultFileByWebApi logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion

        #region private methods

        private IWorkbook ReadFile(string filePath)
        {
            IWorkbook wk = null;
            string extension = System.IO.Path.GetExtension(filePath);
            try
            {
                FileStream fs = File.OpenRead(filePath);
                if (extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    wk = new XSSFWorkbook(fs);
                }

                fs.Close();
                return wk;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<EmployeeInfo> GetEmployeeList()
        {
            List<EmployeeInfo> list = new List<EmployeeInfo>();
            IWorkbook workbookSource = this.ReadFile(SourceDataFilePath);
            ISheet sheet = workbookSource.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    EmployeeInfo info = new EmployeeInfo();
                    info.FirstName = row.GetCell(0).StringCellValue.Trim();
                    info.LastName = row.GetCell(1).StringCellValue.Trim();
                    int annualSalary = 0;
                    int.TryParse(row.GetCell(2).NumericCellValue.ToString(), out annualSalary);
                    info.AnnualSalary = annualSalary;
                    info.SuperRate = row.GetCell(3).NumericCellValue;
                    list.Add(info);
                }
            }
            return list;
        }

        private void GenerateResult(List<PayslipInfo> list)
        {
            IWorkbook workbookSource = this.ReadFile(SourceDataFilePath);
            ISheet sheet = workbookSource.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    //EmployeeInfo info = new EmployeeInfo();
                    //info.FirstName = row.GetCell(0).StringCellValue.Trim();
                    //info.LastName = row.GetCell(1).StringCellValue.Trim();
                    //int annualSalary = 0;
                    //int.TryParse(row.GetCell(2).NumericCellValue.ToString(), out annualSalary);
                    //info.AnnualSalary = annualSalary;
                    //info.SuperRate = row.GetCell(3).NumericCellValue;
                    PayslipInfo info = list[i - 1];
                    for (int j = 4; j < 10; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if(cell == null)
                        {
                            row.CreateCell(j);
                        }
                    }
                    row.GetCell(4).SetCellValue(info.Employee.FullName);
                    row.GetCell(5).SetCellValue(info.PayPeriod);
                    row.GetCell(6).SetCellValue(info.GrossIncome);
                    row.GetCell(7).SetCellValue(info.IncomeTax);
                    row.GetCell(8).SetCellValue(info.NetIncome);
                    row.GetCell(9).SetCellValue(info.Super);
                }
            }
            using (FileStream file = new FileStream(Path.Combine(this.ResultOutputPath, "EmployeeSalaryData" + "_" + Guid.NewGuid().ToString().GetHashCode() + ".xlsx"), FileMode.Create))
            {
                workbookSource.Write(file);
                file.Close();
                MessageBox.Show("Done!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        #endregion


        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{
        //    return base.OnBindedToView(view, oldValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewLoad(view);
        //}

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion


    }

}

