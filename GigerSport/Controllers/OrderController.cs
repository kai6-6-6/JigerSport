﻿using GigerSport.DBModel;
using GigerSport.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigerSport.Controllers
{
    public class OrderController : Controller
    {
        private GetOrderService GetOrderservice = new GetOrderService();
        [Authorize]
        public ActionResult CreateOrder()
        {
            CreateProductListService _GetProduct = new CreateProductListService();
            var GetProduct = _GetProduct.GetProductList();
            return View(GetProduct);
        }

        [HttpPost]
        public ActionResult CreateOrder(string Name,string Phone,string Address,string Email,string Tex,string Department, string FrontWord,int FrontWordSize, string BackWord,int BackWordSize, string Major, int Quantity,double Discount, string Img,int ChineseFontWord,int EngilshFontWord,int FontColor,int NumberFontWord,int Style, string[] PlayerNumber,string[] PlayerName,bool[] LeaderMark,int[] PlayerSize)
        {
            OrderInToDBService intoDB = new OrderInToDBService();
            intoDB.InToDB(Name, Phone, Address, Email, Tex, Department, FrontWord, FrontWordSize, BackWord, BackWordSize, Major, Quantity, Discount, Img, ChineseFontWord, EngilshFontWord, FontColor, NumberFontWord, Style, PlayerNumber, PlayerName, LeaderMark, PlayerSize);
            return RedirectToAction("UnDoneOrderItem");
        }
        [Authorize]
        public ActionResult DoneOrderItems()
        {
            var OrderItem = GetOrderservice.DoneOrderItem();
            return View(OrderItem);
        }
        [Authorize]
        public ActionResult DoneOrderDetail(int orderNumber)
        {
            var OrderDetail = GetOrderservice.DoneOrderDetail(orderNumber);
            return View(OrderDetail);
        }
        [Authorize]
        public ActionResult UnDoneOrderItem()
        {
            var OrderItem = GetOrderservice.UnDoneOrderItem();
            return View(OrderItem);
        }
        [Authorize]
        public ActionResult UnDoneOrderDetail(int orderNumber)
        {
            var OrderDetail = GetOrderservice.EditOrderDetail(orderNumber);
            return View(OrderDetail);
        }
        [HttpPost]
        public ActionResult UnDoneOrderDetail(string Name, string Phone, string Address, string Email, string Tex, string Department, string FrontWord, int FrontWordSize, string BackWord, int BackWordSize, string Major, int Quantity, double Discount, string Img, int ChineseFontWord, int EngilshFontWord, int FontColor, int NumberFontWord, int Style, string[] PlayerNumber, string[] PlayerName, bool[] LeaderMark, int[] PlayerSize,int OrderDetailId)
        {
            SaveDetailChangeService SaveEdit = new SaveDetailChangeService();
            SaveEdit.Save(Name, Phone, Address, Email, Tex, Department, FrontWord, FrontWordSize, BackWord, BackWordSize, Major, Quantity, Discount, Img, ChineseFontWord, EngilshFontWord, FontColor, NumberFontWord, Style, PlayerNumber, PlayerName, LeaderMark, PlayerSize, OrderDetailId);
            return RedirectToAction("UnDoneOrderItem");
        }
        [Authorize]
        public ActionResult DeleteDetail(int orderDetailId)
        {
            DeleteOrderService deleteOrder = new DeleteOrderService();
            deleteOrder.DeleteTarget(orderDetailId);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult FinishDetail(int orderDetailId)
        {
            GigerSportDB context = new GigerSportDB();
            var GetOrderNumber = context.orderDetail.Where((x) => x.orderDetailId == orderDetailId).Select((x) => x.orderNumber).First();
            context.order.Where((x) => x.orderNumber == GetOrderNumber).First().done = true;
            context.SaveChanges();
            return RedirectToAction("DoneOrderItems");
        }
    }
}
