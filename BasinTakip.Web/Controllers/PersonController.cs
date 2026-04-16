using BasinTakip.Core;
using BasinTakip.Core.Business;
using BasinTakip.Core.Data;
using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Web.Models;
using AutoMapper;
using BasinTakip.Domain.Entities.Abstract;
using System.Configuration;
using BasinTakip.EntityFramework.Repository;

namespace BasinTakip.Web.Controllers
{
    public class PersonController : BaseController<IPersonManager, IPersonRepository, PressMember, int>
    {
        public PersonController(IPersonManager manager)
            : base(manager)
        {
        }
        public override ActionResult Detail(DetailInput<int> input = null)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.BackClass = "vievbag_detail";
            ViewBag.IsFileUploaded = "false";
            ViewBag.BlockPress = input.Id;

            ViewBag.Back = "/Person/List";
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            { ViewBag.duplicate ="";

                if (entity.Block == true) { ViewBag.Block = "Kara Listeden Çıkar"; ViewBag.BlockSure = "Mensubu Kara Listeden çıkarmak istediğinize emin misiniz?"; } else { ViewBag.Block = " Kara Listeye Ekle"; ViewBag.BlockSure = "Mensubu Kara Listeye eklemek istediğinize emin misiniz?"; }
                ViewBag.DateTime = entity.CreatedAt.ToString("dd/MM/yyyy");
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            if (entity == null) { entity = new PressMember(); ViewBag.Export = "display:none;"; ViewBag.duplicate = "mailduplicate"; }
            var EditionManager = IocManager.Resolve<IEditionManager>();
            var firmList = EditionManager.Filter(p => p.IsDeleted == false);
            var taskManager = IocManager.Resolve<IPickListManager>();
            var taskList = taskManager.Filter(x => x.CategoryId == 1 && x.IsDeleted == false);
            var model = Mapper.Map<PressMemberDetailModel>(entity);
            var genderManager = IocManager.Resolve<IPickListManager>();
            var genderList = genderManager.Filter(x => x.CategoryId == 5 && x.IsDeleted == false);

            model.ContactRecordBackList = myManager.GetFilterContactRecord(model.Id);
            CityRepository countryRepository = new CityRepository();

            model.CountryList = countryRepository.All().OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.DistrictId
            }).ToList();
            DistrictRepository districtRepository = new DistrictRepository();
            model.DistrictList = districtRepository.All().OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.DistrictId,
                Disabled = p.Id != entity.DistrictId
            }).ToList();
           var list = firmList.OrderBy(x=>x.Name).Select(p => new
            {
             EditionId=p.Id,
             EditionName=p.Name
            }).ToList();
            string[] test;
            if (entity.EditionIds != null)
            {
                test = entity.EditionIds.Split(',');
            }
            else
            {
                test = null;
            }
            model.SelectedValues = test;
            //var list = firmList.Select(p => new SelectListItem
            //{
            //    Text = p.Name,
            //    Value = p.Id.ToString(),
            //    Selected = p.Id == entity.EditionId
            //}).ToList();

            ViewBag.EditionList = new MultiSelectList(list, "EditionId", "EditionName");

         
            model.TaskList = taskList.OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.TaskId
            }).ToList();
            if (model.BirthDate == DateTime.MinValue)
            {

            }

            return View(model);
        }
        public override ActionResult List(GenericListInput<PressMember, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            if (input.Block!=null) ViewBag.Blocks = input.Block.ToString();
            if (input.Block == true) { @ViewBag.btnBlockAll = "btnBlockOff";} else { @ViewBag.btnBlockFilter = "btnBlockOff";  }
            ViewBag.btnNew = "/Person/Detail";
            ViewBag.button = "btn-add";
            ViewBag.BackClass = "viewbag_Back";
            ViewBag.QueryString = "Block=" + input.Block + "&BeginYear=" + input.BeginYear + "&BeginMonth=" + input.BeginMonth + "&CityId=" + input.CityId + "&DistrictId=" + input.DistrictId + "&EditionId=" + input.Edition + "&TaskId=" + input.TaskId + "&SearchText=" + input.SearchText+"&orderByColumn="+input.OrderByColumn+"&OrderType="+input.OrderType;
            var editionRepository = IocManager.Resolve<IEditionManager>();
            ViewBag.EditionList = editionRepository.Filter(p => p.IsDeleted == false).OrderBy(x=>x.Name);
            var taskRepository = IocManager.Resolve<IPickListManager>();
            ViewBag.TaskList = taskRepository.Filter(x => x.CategoryId == 1 && x.IsDeleted==false).OrderBy(x=>x.Name);
            var cityRepository = new CityRepository();
            ViewBag.CityList = cityRepository.All();
            //if (input.BeginYear != null & input.BeginMonth != null)
            //{
            //    input.ContactDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);
            //}
            var result = myManager.GetInclueded(input.PageNumber, input.PageSize, input.ContactDate, input.Edition,
                input.TaskId, input.CityId,input.DistrictId, input.Block, input.OrderByColumn ?? "Id", input.OrderType, input.SearchText,input.BeginYear,input.BeginMonth);
            string[] filter = new string[10];
            filter[0] = input.BeginYear.ToString();
            filter[1] = input.BeginMonth.ToString();
            filter[2] = input.CityId.ToString();
            filter[3] = input.DistrictId.ToString();
            filter[4] = input.Edition.ToString();
            filter[5] = input.TaskId.ToString();
            filter[6] = input.SearchText;
            filter[7] = input.Block.ToString();
            filter[8] = input.OrderByColumn;
            filter[9] = input.OrderType.ToString();
            var model = new GenericListOutput<PersonResult, int>
            {
                SearchText = input.SearchText,
                PagedData = result,
                filter=filter
            };
            return View(model);
        }
        public ActionResult Delete(int Id)
        {
        //    if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
        //    Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
        //    HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
        //    cookie.Expires = DateTime.Now.AddMinutes(20);
        //    HttpContext.Response.Cookies.Add(cookie);
            myManager.DeleteByKey(Id);
            return RedirectToAction("List");
        }
        public ActionResult BlockAdd(int id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.Filter(x => x.Id == id && x.IsDeleted==false).SingleOrDefault();
            if (result.Block == true) result.Block = false; else result.Block = true;
            myManager.Save(result);
            DetailInput<int> input = new DetailInput<int>();
            input.Id = id;
            return RedirectToAction("Detail", "Person", input);
        }

        public override ActionResult Detail(PressMember entity)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var EditionManagerList = IocManager.Resolve<IEditionManager>();
            if (entity.Block == null)
            {
                entity.Block = false;
                var result = myManager.Filter(x => x.Email == entity.Email);
                if (result.Any())
                {
                    ViewBag.Export = "display:none;";
                    ViewBag.Error1 = "Sistemimizde kayıtlı ";
                    ViewBag.Error2 = " hesabı zaten mevcut!";
                    ViewBag.ErrorMail = entity.Email;
                    var EditionManager = IocManager.Resolve<IEditionManager>();
                    var firmList = EditionManager.Filter(p => p.IsDeleted == false);
                    var taskManager = IocManager.Resolve<IPickListManager>();
                    var taskList = taskManager.Filter(x => x.CategoryId == 1 && x.IsDeleted == false);
                    var model = Mapper.Map<PressMemberDetailModel>(entity);
                    var genderManager = IocManager.Resolve<IPickListManager>();
                    var genderList = genderManager.Filter(x => x.CategoryId == 5 && x.IsDeleted == false);

                    model.ContactRecordBackList = myManager.GetFilterContactRecord(model.Id);
                    CityRepository countryRepository = new CityRepository();

                    model.CountryList = countryRepository.All().OrderBy(x=>x.Name).Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString(),
                        Selected = p.Id == entity.DistrictId
                    }).ToList();
                    DistrictRepository districtRepository = new DistrictRepository();
                    model.DistrictList = districtRepository.All().OrderBy(x=>x.Name).Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString(),
                        Selected = p.Id == entity.DistrictId,
                        Disabled = p.Id != entity.DistrictId
                    }).ToList();
                    model.FirmList = firmList.OrderBy(x=>x.Name).Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString(),
                        Selected = p.Id == entity.EditionId
                    }).ToList();
                    model.TaskList = taskList.OrderBy(x=>x.Name).Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString(),
                        Selected = p.Id == entity.TaskId
                    }).ToList();
                    if (model.BirthDate == DateTime.MinValue)
                    {

                    }
                    model.FirstName = entity.FirstName;
                    model.LastName = entity.LastName;
                    model.Adress = entity.Adress;
                    model.About = entity.About;
                    model.Email = entity.Email;
                    return View(model);
                }
            }
        
                myManager.Save(entity);
                return RedirectToAction("List");
           

        }


        public ActionResult DetailPressMember(PressMemberDetailModel entitys)
        {
            PressMember entity = new PressMember();
            entity.Id = entitys.Id;
            entity.FirstName = entitys.FirstName;
            entity.LastName = entitys.LastName;
            entity.IsDeleted = entitys.IsDeleted;
            entity.MobilePhone = entitys.MobilePhone;
            entity.ModifiedAt = entitys.ModifiedAt;
            entity.Order = entitys.Order;
            entity.Path1 = entitys.Path1;
            entity.Path2 = entitys.Path2;
            entity.Path3 = entitys.Path3;
            entity.Path4 = entitys.Path4;
            entity.Permalink = entitys.Permalink;
            entity.TaskId = entitys.TaskId;
            entity.EditionId = entitys.EditionId;
            entity.EditionIds = entitys.EditionIds;
            entity.EditionNames = entitys.EditionNames;
            entity.Email = entitys.Email;
            entity.Email2 = entitys.Email2;
            entity.Fax = entitys.Fax;
            entity.DistrictId = entitys.DistrictId;
            entity.CountryId = entitys.CountryId;
            entity.Avatar = entitys.Avatar;
            entity.Adress = entitys.Adress;
            entity.About = entitys.About;
            entity.BirthDate = entitys.BirthDate;
            entity.Block = entitys.Block;
            entity.CreatedMemberId = entitys.CreatedMemberId;
            entity.DeletedAt = entitys.DeletedAt;
            entity.DeletedMemberId = entitys.DeletedMemberId;
            entity.IsActive = entitys.IsActive;
            entity.Notes = entitys.Notes;

            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            bool durum = true;bool editionnamestatus = true;
        
                for (int i = 0; i < entitys.SelectedValues.Count(); i++)
                {
                    if (durum)
                    {
                        entity.EditionIds = entitys.SelectedValues[i].ToString();
                        durum = false;
                    }
                    else
                    {
                        entity.EditionIds += "," + entitys.SelectedValues[i].ToString();
                    }
                
            }
            using (IocManager.BeginScope())
            {
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var selectedEditionList = (from editionlist in editionRepository.All()
                                          where entitys.SelectedValues.Contains(editionlist.Id.ToString())
                                          select editionlist).ToList();
                for (int i = 0; i < selectedEditionList.Count(); i++)
                {
                    if (editionnamestatus)
                    {
                        entity.EditionNames = selectedEditionList[i].Name.ToString();
                        editionnamestatus = false;
                    }
                    else
                    {
                        entity.EditionNames += "," + selectedEditionList[i].Name.ToString();
                    }

                }
            }
            var EditionManagerList = IocManager.Resolve<IEditionManager>();
              if (entity.Block == null)
             {   
               entity.Block = false;
            //var result = myManager.Filter(x => x.Email == entity.Email);
            //if (result.Any())
            //{
            //    ViewBag.Export = "display:none;";
            //    ViewBag.Error1 = "Sistemimizde kayıtlı ";
            //    ViewBag.Error2 = " hesabı zaten mevcut!";
            //    ViewBag.ErrorMail = entity.Email;
            //    var EditionManager = IocManager.Resolve<IEditionManager>();
            //    var firmList = EditionManager.Filter(p => p.IsDeleted == false);
            //    var taskManager = IocManager.Resolve<IPickListManager>();
            //    var taskList = taskManager.Filter(x => x.CategoryId == 1 && x.IsDeleted == false);
            //    var model = Mapper.Map<PressMemberDetailModel>(entity);
            //    var genderManager = IocManager.Resolve<IPickListManager>();
            //    var genderList = genderManager.Filter(x => x.CategoryId == 5 && x.IsDeleted == false);

            //    model.ContactRecordBackList = myManager.GetFilterContactRecord(model.Id);
            //    CityRepository countryRepository = new CityRepository();

            //    model.CountryList = countryRepository.All().Select(p => new SelectListItem
            //    {
            //        Text = p.Name,
            //        Value = p.Id.ToString(),
            //        Selected = p.Id == entity.DistrictId
            //    }).ToList();
            //    DistrictRepository districtRepository = new DistrictRepository();
            //    model.DistrictList = districtRepository.All().Select(p => new SelectListItem
            //    {
            //        Text = p.Name,
            //        Value = p.Id.ToString(),
            //        Selected = p.Id == entity.DistrictId,
            //        Disabled = p.Id != entity.DistrictId
            //    }).ToList();
            //    model.FirmList = firmList.Select(p => new SelectListItem
            //    {
            //        Text = p.Name,
            //        Value = p.Id.ToString(),
            //        Selected = p.Id == entity.EditionId
            //    }).ToList();
            //    model.TaskList = taskList.Select(p => new SelectListItem
            //    {
            //        Text = p.Name,
            //        Value = p.Id.ToString(),
            //        Selected = p.Id == entity.TaskId
            //    }).ToList();
            //    if (model.BirthDate == DateTime.MinValue)
            //    {

            //    }
            //    model.FirstName = entity.FirstName;
            //    model.LastName = entity.LastName;
            //    model.Adress = entity.Adress;
            //    model.About = entity.About;
            //    model.Email = entity.Email;
            //    return View(model);
            //}
            }
            entity.EditionId = 1;
            myManager.Save(entity);
            return RedirectToAction("List");


        }

    }
}