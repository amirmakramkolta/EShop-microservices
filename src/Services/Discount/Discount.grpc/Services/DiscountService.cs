using Discount.grpc.Data;
using Discount.grpc.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new() { ProductName = "No Disscount", Amount = 0, Description = "No Coupount Description", Id = 0 };
            }

            logger.LogInformation("Discount retrive for product: {productname}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Coupon has been created for {productName} ", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, detail: "Invalid request"));
            if(!dbContext.Coupons.Any(x=>x.Id == coupon.Id))
                throw new RpcException(new Status(StatusCode.NotFound, detail: "Invalid Id"));


            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Coupon has been updated for {productName} ", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(x => x.ProductName == request.ProductName);
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with name {request.ProductName} not found"));
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse() { Success = true };
            
        }
    }
}
