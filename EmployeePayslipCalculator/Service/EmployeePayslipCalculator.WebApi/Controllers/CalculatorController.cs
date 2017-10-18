using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeePayslipCalculator.Service;
using EmployeePayslipCalculator.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeePayslipCalculator.WebApi.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly PayslipCalculatorService service;

        public CalculatorController(PayslipCalculatorService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Calculate(int month, [FromBody] EmployeeInfo item)
        {
            ResponseResult<PayslipInfo> response = new ResponseResult<PayslipInfo>();
            try
            {
                if (item == null)
                {
                    return BadRequest();
                }
                var result = this.service.Calculate(item, month);
                response.Result = result;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult BatchCalculate(int month, [FromBody] List<EmployeeInfo> items)
        {
            ResponseResult<List<PayslipInfo>> response = new ResponseResult<List<PayslipInfo>>();
            try
            {
                if (items == null)
                {
                    return BadRequest();
                }
                var result = this.service.Calculate(items, month);
                response.Result = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }
    }
}
