using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommandScheduler.Presentation.Presenters;
using CommandScheduler.Presentation.Views;

namespace CommandScheduler.UserInterface.Web.CodeFiles
{
    public abstract class PageBase<TPresenter, TView> : System.Web.UI.Page
            where TView : IView
            where TPresenter : Presenter<TView>
    {
      
        protected abstract ResourceManager ResMgr { get; }

        public TPresenter Presenter { get; set; }

        public abstract void AttachView();

        protected override void OnPreInit(EventArgs e)
        {
            AttachView();
            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            Presenter.OnViewInitializing();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {            
            Presenter.OnViewLoading();

            if (!Page.IsPostBack)
                Presenter.OnViewInitialized();

            base.OnLoad(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            Presenter.OnViewLoaded();
            base.OnLoadComplete(e);
        }



    }
}