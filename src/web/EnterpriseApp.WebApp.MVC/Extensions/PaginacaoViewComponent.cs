using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedViewModel modeloPaginado)
            => View(modeloPaginado);
    }
}
