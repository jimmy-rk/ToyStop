using EcommerceSite.Entities;
using EcommerceSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceSite.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        public ActionResult Index()
        {
            var model = ConfigurationsService.Instance.GetConfigs();
            return View(model);
        }
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Config configuration)
        {
            if (ModelState.IsValid)
            {
                ConfigurationsService.Instance.SaveConfiguration(configuration);
                return RedirectToAction("Index");
            }
            return View(configuration);
        }

        [HttpGet]
        public ActionResult Edit(string key)
        {
            var configuration = ConfigurationsService.Instance.GetConfig(key);
            return View(configuration);
        }

        [HttpPost]
        public ActionResult Edit(Config configuration)
        {
            if (ModelState.IsValid)
            {
                ConfigurationsService.Instance.UpdateConfiguration(configuration);
                return RedirectToAction("Index");
            }

            return View(configuration);
        }
        [HttpGet]
        public ActionResult Delete()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string key)
        {
            ConfigurationsService.Instance.DeleteConfiguration(key);
            return RedirectToAction("Index");
        }
    }
}