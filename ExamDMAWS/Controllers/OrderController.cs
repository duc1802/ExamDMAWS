using ExamDMAWS.DTOs;
using ExamDMAWS.Entities;
using ExamDMAWS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamDMAWS.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<OrderDTO> ls = _context.OrderTbl
                .Select(m => new OrderDTO
                {
                    OrderId = m.OrderId,
                    ItemCode = m.ItemCode,
                    ItemName = m.ItemName,
                    ItemQty =m.ItemQty,
                    OrderDelivery = m.OrderDelivery,
                    OrderAddress = m.OrderAddress,
                    PhoneNumber = m.PhoneNumber
                })
                .ToList();
            return Ok(ls);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order order = new Order { ItemName = model.ItemName };
                    _context.OrderTbl.Add(order);
                    _context.SaveChanges();
                    return Created("", new OrderDTO
                    {
                        OrderId = order.OrderId,
                        ItemCode = order.ItemCode,
                        ItemName = order.ItemName,
                        ItemQty = order.ItemQty,
                        OrderDelivery = order.OrderDelivery,
                        OrderAddress = order.OrderAddress,
                        PhoneNumber = order.PhoneNumber
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Error");
        }


        [HttpPut]
        public IActionResult Edit(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.OrderTbl.Update(new Order
                    {
                        OrderId = model.OrderId,
                        ItemCode = model.ItemCode,
                        ItemName = model.ItemName,
                        ItemQty = model.ItemQty,
                        OrderDelivery = model.OrderDelivery,
                        OrderAddress = model.OrderAddress,
                        PhoneNumber = model.PhoneNumber
                    });
                    _context.SaveChanges();
                    return Ok("Successfully");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest("Error");
        }

        [HttpDelete]
        [Authorize(Policy = "SuperAdmin")]
        public IActionResult Delete(int OrderId)
        {
            try
            {
                Order category = _context.OrderTbl.Find(OrderId);
                _context.OrderTbl.Remove(order);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
