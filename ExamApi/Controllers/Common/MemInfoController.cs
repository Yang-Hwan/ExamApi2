
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ExamApi.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamModel;

namespace ExamApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemInfoController : ControllerBase
    {
        private readonly OhContext _context;
        public MemInfoController(OhContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<MemInfo> GetMems()
        {
            var mem = _context.MemInfo;

            return Ok(mem);
        }

        [HttpGet]
        [Route("mem")]
        public ActionResult<MemWalletLog> GetMem01()
        {
            var wallet = _context.MemWalletLog;

            return Ok(wallet);
        }

        [HttpGet("wallet")]
        public async Task<ActionResult<List<WalletInfo>>> WalletInfo()
        {
            List<WalletInfo> wallet = await (from mem in _context.MemInfo
                         join wal in _context.MemWallet on mem.MemIdx equals wal.MemIdx into mw from w in mw.DefaultIfEmpty()
                         where mem.MemIdx > 100
                         select new WalletInfo() { MemIdx = mem.MemIdx, Point = w.MemIdx > 0 ? w.Point : 0 }).ToListAsync();
            return Ok(wallet);
        }

        [HttpGet("wallet2")]
        public ActionResult WalletInfo2()
        {
            var wallet = from mem in _context.MemInfo
                join wal in _context.MemWallet on mem.MemIdx equals wal.MemIdx into mw
                from w in mw.DefaultIfEmpty()
                where mem.MemIdx > 100
                select new { MemIdx = mem.MemIdx, Point = w.MemIdx > 0 ? w.Point : 0 };

            return Ok(wallet);
        }


        [HttpGet("menu")]
        public ActionResult Menu(int MemIdx)
        {
            var menu = from ma in _context.MenuAuth
                       join mi in _context.MemInfo 
                       on new { ma.Auth, MemIdx } 
                       equals new { mi.Auth, mi.MemIdx  }  
                       where ma.UpMark == ""
                       select new
                       {
                           Auth =ma.Auth,
                           Mark = ma.Mark,
                           UpOrd = ma.UpOrd,
                           Ord = ma.Ord,
                           IsUse = ma.IsUse,
                       };

            return Ok(menu);
        }

        [HttpGet("menu3")]
        public ActionResult Menu3(int MemIdx)
        {
            var menu = _context.MenuAuth
                .Where(x=>x.UpMark == "")
                .GroupJoin(
                    _context.MemInfo,
                    ma => new { ma.Auth, MemIdx},
                    mi => new { mi.Auth, mi.MemIdx },
                    (mauth, minfo) => new {mauth, minfo})
                .SelectMany(
                    ai => ai.minfo,    
                    (ai, i) => new
                    {
                        Auth = ai.mauth.Auth,
                        Mark = ai.mauth.Mark,
                        UpOrd = ai.mauth.UpOrd,
                        Ord = ai.mauth.Ord,
                        IsUse = ai.mauth.IsUse
                    });



            return Ok(menu);

        }

        [HttpGet("menu2")]
        public ActionResult Menu2(int Step)
        {
            var menu = _context.Menu.Where(x => x.Step == Step).Select(r=> new
            {
                Mark = r.Mark,
                MarkRef = r.MarkRef,
                Step = r.Step,
                Src = r.Src 
            });

            return Ok(menu);
        }

        [HttpGet("menu2-1")]
        public ActionResult Menu2_1(int Step)
        {
            var menu = _context.Menu.Where(x => x.Step == Step).Select(r => new
            {
                Mark = r.Mark,
                MarkRef = r.MarkRef,
                Step = r.Step,
                Src = r.Src
            });

            return Ok(menu);
        }

    }

    public class WalletInfo
    {
        public int MemIdx { get; set; }

        public int Point { get; set; }
    }
}
