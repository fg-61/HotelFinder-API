using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]     // auto validation control
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Hotel> hotels = _hotelService.GetAllHotels();
            return Ok(hotels);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);   // 200 + data
            }
            return NotFound();  // 404
        }
        [HttpPost]
        public IActionResult Post([FromBody]Hotel hotel)
        {
            var createdHotel = _hotelService.CreateHotel(hotel);
            return CreatedAtAction("GetById", new { id = createdHotel.Id }, createdHotel);  // 201 + data
        }
        [HttpPut]
        public IActionResult Put([FromBody]Hotel hotel)
        {
            var updatedHotel = _hotelService.GetHotelById(hotel.Id);
            if(updatedHotel != null)
            {
                _hotelService.UpdateHotel(updatedHotel);
                return Ok(updatedHotel);    // 200 + data
            }
            return NotFound();    // 404
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedHotel = _hotelService.GetHotelById(id);
            if(deletedHotel != null)
            {
                _hotelService.DeleteHotel(deletedHotel.Id);
                return Ok(deletedHotel);    // 200
            }
            
            return NotFound();  // 404 
        }

    }
}
