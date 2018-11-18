using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommandScheduler.Presentation.Views;
using CommandScheduler.Presentation.Presenters;
using CommandScheduler.Presentation.ViewModels;

namespace CommandScheduler.UserInterface.Web.CodeFiles
{

    public abstract class CRUDPage<TModel, TView, TPresenter> : PageBase<TPresenter, TView>
        where TModel : ViewModelBase
        where TView : IView
        where TPresenter : Presenter<TModel, TView>
    {        
        protected abstract Store StoreGrid { get; }
        protected abstract GridPanel GridPanel { get; }

        protected virtual void btnSave_Click(object sender, DirectEventArgs e)
        {
            string error = string.Empty;

            try
            {
                ChangeRecords<TModel> changes = GetChangedRecords(e);
                Presenter.SaveModel(changes.Created, changes.Updated, changes.Deleted);
            }
            catch (Exception ex) //change to crud ex
            {
                error = ex.Message;
            }

            ResMgr.AddScript(error == string.Empty ? "SaveSuccess();" : string.Format("SaveFailure('{0}');", error));
        }
        protected virtual void btnDelete_Click(object sender, DirectEventArgs e)
        {
            string error = string.Empty;

            try
            {

                ChangeRecords<TModel> changes = GetChangedRecords(e);
                Presenter.SaveModel(null, null, changes.Deleted);

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            ResMgr.AddScript(error == string.Empty ? "DeleteSuccess();" : "DeleteFailure();");
        }
        protected virtual void storeGrid_ReadData(object sender, StoreReadDataEventArgs e)
        {
            StoreGrid.DataSource = Presenter.GetModelForGridDisplay();
            StoreGrid.DataBind();
        }
        protected void SetGridFieldVisiblity()
        {
            var hiddenFields = GetInVisibleFields(typeof(TModel).Name);
            foreach (var hiddenField in hiddenFields)
            {
                var gridColumn = GridPanel.ColumnModel.GetColumnByDataIndex(hiddenField);
                if (gridColumn != null)
                    gridColumn.Hidden = true;
            }
        }
        private List<string> GetInVisibleFields(string name)
        {
            throw new NotImplementedException();
        }
        private ChangeRecords<TModel> GetChangedRecords(DirectEventArgs e)
        {
            StoreDataHandler dataHandler = new StoreDataHandler(e.ExtraParams["changedData"]);
            return dataHandler.BatchObjectData<TModel>();
        }
    }
}