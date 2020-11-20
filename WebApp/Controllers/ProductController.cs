using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;

namespace 
{
public class ProductController : LocalizedController
{
[RadynAuthorize]
public ActionResult Index()
{
var list = dboComponent.Instance.ProductFacade.GetAll();
 if (list.Count == 0) return this.Redirect("~/dbo/Product/Create"); 
return View(list);
}

[RadynAuthorize]
public ActionResult Details(Int32 Id)
{
return View(dboComponent.Instance.ProductFacade.Get(Id ));
}

[RadynAuthorize]
public ActionResult Create()
{
return View(new Product());
}

[HttpPost]
public ActionResult Create(FormCollection collection)
{
var product = new Product();
try
{
this.RadynTryUpdateModel(product);
if (dboComponent.Instance.ProductFacade.Insert(product))
{
ShowMessage(Resources.Common.InsertSuccessMessage,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Succeed);
return (!string.IsNullOrEmpty(Request.QueryString["AddNew"]))?this.Redirect("~/dbo/Product/Create"): this.Redirect("~/dbo/Product/Index");
}
ShowMessage(Resources.Common.ErrorInInsert,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return this.Redirect("~/dbo/Product/Index");
}
catch(Exception exception)
{
ShowMessage(Resources.Common.ErrorInInsert+exception.Message,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return View(product);
}
}

[RadynAuthorize]
public ActionResult Edit(Int32 Id)
{
return View(dboComponent.Instance.ProductFacade.Get(Id ));
}

[HttpPost]
public ActionResult Edit(Int32 Id, FormCollection collection)
{
var product = dboComponent.Instance.ProductFacade.Get(Id );
try
{
this.RadynTryUpdateModel(product);
if (dboComponent.Instance.ProductFacade.Update(product))
{
ShowMessage(Resources.Common.UpdateSuccessMessage,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Succeed);
return this.Redirect("~/dbo/Product/Index");
}
ShowMessage(Resources.Common.ErrorInEdit,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return this.Redirect("~/dbo/Product/Index");
}
catch(Exception exception)
{
ShowMessage(Resources.Common.ErrorInEdit+exception.Message,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return View(product);
}
}

[RadynAuthorize]
public ActionResult Delete(Int32 Id)
{
return View(dboComponent.Instance.ProductFacade.Get(Id ));
}

[HttpPost]
public ActionResult Delete(Int32 Id, FormCollection collection)
{
var product = dboComponent.Instance.ProductFacade.Get(Id );
try
{
if (dboComponent.Instance.ProductFacade.Delete(Id ))
{
ShowMessage(Resources.Common.DeleteSuccessMessage,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Succeed);
return this.Redirect("~/dbo/Product/Index");
}
ShowMessage(Resources.Common.ErrorInDelete,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return this.Redirect("~/dbo/Product/Index");
}
catch(Exception exception)
{
ShowMessage(Resources.Common.ErrorInDelete+exception.Message,Resources.Common.MessaageTitle,messageIcon:MessageIcon.Error);
return View(product);
}
}
}
}
