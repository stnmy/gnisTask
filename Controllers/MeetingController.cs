using Microsoft.AspNetCore.Mvc;
using gnisTask.Interface;
using gnisTask.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace gnisTask.Controllers
{
    public class MeetingController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(
            IMeetingRepository meetingRepository
            )
        {
            _meetingRepository = meetingRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCorporateCustomers()
        {
            try
            {
                var customers = await _meetingRepository.GetCorporateCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error loading corporate customers");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetIndividualCustomers()
        {
            try
            {
                var customers = await _meetingRepository.GetIndividualCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error loading individual customers");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsServices()
        {
            try
            {
                var products = await _meetingRepository.GetProductsServices();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error loading products/services");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMeetingMaster([FromBody] MeetingFormDto formData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { success = false, message = "Validation failed", errors });
            }

            try
            {
                var meetingMaster = new MeetingMaster
                {
                    CustomerType = formData.CustomerType,
                    CustomerId = formData.CustomerId,
                    MeetingDate = formData.MeetingDate,
                    MeetingTime = formData.MeetingTime,
                    MeetingPlace = formData.MeetingPlace,
                    AttendsFromClientSide = formData.AttendsFromClientSide,
                    AttendsFromHostSide = formData.AttendsFromHostSide,
                    MeetingAgenda = formData.MeetingAgenda,
                    MeetingDiscussion = formData.MeetingDiscussion,
                    MeetingDecision = formData.MeetingDecision
                };

                var meetingId = await _meetingRepository.SaveMeetingMaster(meetingMaster);

                if (formData.ProductDetails != null && formData.ProductDetails.Any())
                {
                    foreach (var detail in formData.ProductDetails)
                    {
                        detail.MeetingId = meetingId;
                    }
                    await _meetingRepository.SaveMeetingDetails(formData.ProductDetails);
                }

                return Ok(new { success = true, message = "Meeting and product details saved successfully", meetingId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while saving the meeting data." });
            }
        }
    }
}
