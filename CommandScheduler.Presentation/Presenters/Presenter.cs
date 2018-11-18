using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Presentation.ViewModels;
using CommandScheduler.Presentation.Views;

namespace CommandScheduler.Presentation.Presenters
{

    public abstract class Presenter<TView>
        where TView : IView
    {        
        internal SessionState SessionState { get; set; }
        public TView View { get; set; }

        private AppUser _appUser = null;
        public AppUser AppUser
        {
            get
            {
                if (_appUser == null)
                    _appUser = SessionState.AppUser;
                return _appUser;
            }
        }
        

        #region View Event Virtual Place holders

        public virtual void OnViewInitializing()
        {

        }

        public virtual void OnViewInitialized()
        {
            SessionState.AppUser = new AppUser {  };
        }

        public virtual void OnViewLoading()
        {

        }

        public virtual void OnViewLoaded()
        {

        }

        #endregion
    }

    public abstract class Presenter<TModel, TView> : Presenter<TView>
        where TModel : ViewModelBase
        where TView : IView

    {
        internal abstract ICRUDService<TModel> Service { get; set; }

        public List<TModel> GetModelForGridDisplay()
        {
            return Service.GetList();
        }
        public virtual void SaveModel(List<TModel> created, List<TModel> updated, List<TModel> deleted)
        {            

            if (created != null)
                foreach (var create in created)
                {
                    SetDefaults(create);
                    CreatePreProcessor(create);
                    Service.Insert(create);
                }

            if (updated != null)
                foreach (var update in updated)
                {
                    SetDefaults(update);
                    UpdatePreProcessor(update);
                    Service.Update(update);
                }

            if (deleted != null)
                foreach (var delete in deleted)
                {
                    SetDefaults(delete);
                    DeletePreProcessor(delete);
                    Service.Delete(delete);
                }            
        }

        private void SetDefaults(TModel model)
        {
            
        }

        protected virtual void CreatePreProcessor(TModel entity)
        {
            if (entity.IsAuditable())
            {
                IAuditable auditableEntity = (IAuditable)entity;
                auditableEntity.CreatedBy = AppUser.Username;
                auditableEntity.CreatedOn = DateTime.Now;
            }
        }
        protected virtual void UpdatePreProcessor(TModel entity)
        {
            if (entity.IsAuditable())
            {
                IAuditable auditableEntity = (IAuditable)entity;
                auditableEntity.LastModifiedBy = AppUser.Username;
                auditableEntity.LastModifiedOn = DateTime.Now;
            }
        }
        protected virtual void DeletePreProcessor(TModel entity) { }

    }




}

