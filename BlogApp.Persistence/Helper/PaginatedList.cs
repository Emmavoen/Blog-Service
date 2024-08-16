// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BlogApp.Persistence.DatabaseContext;

// namespace BlogApp.Persistence.Helper
// {
//     public class PaginatedList<T> where T : class
//     {
//         private readonly AppDbContext _dbContext;

//         public PaginatedList(AppDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }
//         public async Task<PaginatedList<T>> GetPaginatedBySenderAccountAsync(string senderAccount, int pageNumber, int pageSize)
//         {
//             var query = _dbContext.data.Where(sp => sp.SenderAccount == senderAccount).AsQueryable();
//             var count = await query.CountAsync();

//             var items = await query.Skip((pageNumber - 1) * pageSize)
//                                    .Take(pageSize)
//                                    .ToListAsync();


//             var dtoItems = items.Select(sp => new SchedulePaymentResponseDto
//                                    {
                                       
//                                        StartDate = sp.StartDate,
//                                        EndDate = sp.EndDate,
//                                        NoOfPayment = sp.NoOfPayment,
//                                        PaymentFrequency = sp.PaymentFrequency,
//                                        Amount = sp.Amount,
//                                        BeneficiaryAccountNumber = sp.BeneficiaryAccountNumber,
//                                        BeneficiaryName = sp.BeneficiaryName,
//                                        Narration = sp.Narration,
//                                        SenderAccount = sp.SenderAccount,
//                                        SenderName = sp.SenderName,
//                                        DateCreated = sp.DateCreated,
//                                        DateUpdated = sp.DateUpdated
//                                     }).ToList();

//             return new PaginatedList<SchedulePaymentResponseDto>(dtoItems, count, pageNumber, pageSize);
//         }

//     }
// }