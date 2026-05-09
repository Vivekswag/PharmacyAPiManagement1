using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;

    public MedicinesController(IMedicineService medicineService)
    {
        _medicineService = medicineService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var medicines = _medicineService.GetAll();

        return Ok(medicines);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Medicine medicine)
    {
        var result = _medicineService.Add(medicine);

        return Ok(result);
    }
}