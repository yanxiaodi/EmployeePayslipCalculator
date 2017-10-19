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

