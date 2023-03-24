using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services
{
    public class PartyService : IPartyService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        public PartyService(SwastiFashionHubLlpContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Guid>> SaveAsync(Party party)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                party.CreatedDate = DateTime.Now;
                party.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Parties.Add(party);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(party.Id, "Party saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(Party party)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                party.UpdatedDate = DateTime.Now;
                party.UpdatedBy = 0; //todo: get logged in user id and set it.

                _context.Entry(party).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(party.Id, "Party updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Party>> GetAsync(Guid id)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Party>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Parties.FindAsync(id);
                if (data == null)
                    return await Result<Party>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<Party>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<Party>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<Party>>> GetAllAsync(string search = "")
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<List<Party>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = _context.Parties.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                var partyQuery = await queryable.AsNoTracking().ToListAsync();
                if (partyQuery == null)
                    return await Result<List<Party>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<List<Party>>.SuccessAsync(partyQuery);

            }
            catch (Exception ex)
            {
                return await Result<List<Party>>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Parties.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                _context.Parties.Remove(data);
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "Party deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }
    }
}
