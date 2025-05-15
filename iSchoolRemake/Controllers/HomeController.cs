using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using iSchoolRemake.Models;
using iSchoolRemake.Services;
using System.Diagnostics;
using System.Dynamic;

namespace iSchoolRemake.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        //need to go get the data
        DataRetrieval dr = new DataRetrieval();
        //tell the instance of dr to go get the data
        var loadedAbout = await dr.GetData("about/");

        // build out model!
        // built, called AboutModel!

        // installed NuGet package NewtonSoft JSON
        var aboutModel = JsonConvert.DeserializeObject<AboutModel>(loadedAbout);
        aboutModel.pageTitle = "Home iSchool";

        //cast data (string) into json
        //need to stuff data into model
        return View(aboutModel);
    }

    public async Task<IActionResult> People()
    {
        DataRetrieval dr = new DataRetrieval();
        var loadedPeople = await dr.GetData("people/");
        var rtnResults = JsonConvert.DeserializeObject<PeopleModel>(loadedPeople);
        rtnResults.pageTitle = "People page!";

        return View(rtnResults);
    }

    public async Task<IActionResult> Degrees()
    {
        //need to go get the data
        DataRetrieval dr = new DataRetrieval();
        //tell the instance of dr to go get the data
        var loadedDegrees = await dr.GetData("degrees/");
        var loadedMinors = await dr.GetData("minors/");

        // build out model!
        // built, called AboutModel!
        // installed NuGet package NewtonSoft JSON
        var degreesModel = JsonConvert.DeserializeObject<DegreeModel>(loadedDegrees);
        var minorsModel = JsonConvert.DeserializeObject<MinorsModel>(loadedMinors);

        // use the expando object to create a dynamic model
        dynamic model = new ExpandoObject();
        model.Degrees = degreesModel;
        model.Minors = minorsModel;
        model.PageTitle = "Degrees/Minors";

        return View(model);
    }

    public async Task<IActionResult> Employment()
    {
        //need to go get the data
        DataRetrieval dr = new DataRetrieval();
        // tell the instance of dr to go get the data
        var loadedEmployment = await dr.GetData("employment/");

        // build out the model
        var employmentModel = JsonConvert.DeserializeObject<EmploymentModel>(loadedEmployment);
        employmentModel.PageTitle = "Employment";
        return View(employmentModel);
    }

    public async Task<IActionResult> Footer()
    {
        DataRetrieval dr = new DataRetrieval();
        // tell instance to get data
        var loadedFooter = await dr.GetData("footer/");
        // convert into C# object model
        var footerModel = JsonConvert.DeserializeObject<FooterModel>(loadedFooter);

        return PartialView("_Footer", footerModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
