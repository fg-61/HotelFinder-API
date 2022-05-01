using HotelFinder.Business.Abstract;
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

        // for using swagger comment -> HotelFinder.API properties -> Build -> Output -> Generate a file containing API documentation (click)

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetHotel()
        {
            List<Hotel> hotels = _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]    // api/hotels/gethotelbyid/2
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);   // 200 + data
            }
            return NotFound();  // 404
        }

        /// <summary>
        /// Get hotel by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")]    // api/hotels/gethotelbyname/titanic
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel);   // 200 + data
            }
            return NotFound();  // 404
        }

        /// <summary>
        /// Create an hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateHotel([FromBody]Hotel hotel)
        {
            var createdHotel = _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);  // 201 + data
        }

        /// <summary>
        /// Update hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateHotel([FromBody]Hotel hotel)
        {
            var updatedHotel = _hotelService.GetHotelById(hotel.Id);
            if(updatedHotel != null)
            {
                _hotelService.UpdateHotel(hotel);
                return Ok(hotel);    // 200 + data
            }
            return NotFound();    // 404
        }

        /// <summary>
        /// Delete hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteHotelById(int id)
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
