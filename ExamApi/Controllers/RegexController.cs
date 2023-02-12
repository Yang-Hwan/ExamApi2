using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ExamApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RegexController : ControllerBase
    {

        [HttpGet("Regex01")]
        public ActionResult<object> Regex01()
        {
            var text = "Hello, 안녕하세요.내 이름은 범조 hello BeoBeaojojo";
            var matches = Regex.Matches(text, @"[h|H]ello");
            var msg = "";

            foreach (Match item in matches)
            {
                msg += $"index = {item.Index}, Len = {item.Length}, Value = {item.Value} ..\r\n";
            }
            msg += "\r\n\r\n";
            Match match = Regex.Match(text, @"[h|H]ello");
            while (match.Success)
            {
                msg += $"index = {match.Index}, Len = {match.Length}, Value = {match.Value} ..\r\n";
                match = match.NextMatch();
            }
            msg += "\r\n\r\n";

            var matches2 = Regex.Matches(text, @"[h|H]ello")
                .Cast<Match>()
                .OrderByDescending(x => x.Index);

            foreach (Match item in matches2)
            {
                msg += $"index = {item.Index}, Len = {item.Length}, Value = {item.Value} ..\r\n";
            }

            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// ? : 직전의 한번 또는 없는 경우.
        /// ^, $ : 시작과 끝 지점을 의미
        /// [-+]? : 시작지점 부호를 의미. ?는 직전의 패턴과 일치하지 않거나 한번만 일치함을 의미
        /// (\d+) : 정수부를 의미 \d문자, + 는 한번 이상반복을 의미, 괄호는 묶음기호, 시작이 0으로 하고 싶지 않다면 ([1-9]\d*)로 표현, +를 사용했으므로 .001 (.)사용 안됨.
        /// (\.\d+)? : 소수부를 의미. \. 마침표를 의미, .만 있으면 임의의 한 문자를 의미하므로 부적합. 마지막의 ? 는 소수부가 없는 경우에 대응.
        /// <returns></returns>
        [HttpGet("Regex02")]
        public ActionResult<object> Regex02()
        {
            var msg = "";
            var array = new[] { "1200", "-30.43", "0.432", "+232.34", "10.3.2", "3210-42123", "$123" };
            var regex = new Regex(@"^[-+]?(\d+)(\.\d+)?$");

            foreach(var item in array)
            {
                var isMatch = regex.IsMatch(item);
                if (isMatch)
                {
                    msg += $"result = {item}\r\n";
                }
            }

            return msg;
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// @"<.+>" : 이 정규식은 최대한 긴 문자열과 일시키려고 하기 때문에 <범범조조> 입니다
        ///         임의의 문자열을 나타내는 점(.)이 <이나 >과도 일치하기 때문입니다.
        /// @"<[^<>]+>" : < 이나 > 와 일치하지 않도록 점(.)이 있는 부분은 <> 이외의 것을 나타내는 [^<>] 로 바꿈 
        /// @"<([^<>]+)>" : 최종적으로 구하려는 문자열을 괄호로 묶어서 그룹화하면 일치한 문자열 안에서 괄호안에 잇는 문자열만 꺼낼수 있습니다.
        /// <returns></returns>
        [HttpGet("Regex03")]
        public ActionResult<object> Regex03()
        {
            var msg = "";
            var text = "Hello, 안녕하세요.내 이름은 <범조> hello BeoBeaojojo 만나서 <반갑습니다> 다음에 또 만나요.";
            var regex = Regex.Matches(text, @"<([^<>]+)>");

            foreach (Match item in regex)
            {
                msg += $"g 0 = {item.Groups[0]}, g 1 = {item.Groups[1]} ..\r\n";
            }

            return msg;
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// [0-9]+ : 숫자로 구성된 문자열 일치
        /// [a-zA-Z0-9]+ : 영문자, 숫자로 구성된 문자열 일치
        /// [!-/:-@[-'{~}]+ : 기호로 구성된 부분 문자열과 일치
        /// \s+ : 공백 외의 임의 문자로 구성된 부분과 일치
        /// \p{IsHangulSyllables}+ 한글로 구성된 부분 문자열과 일치
        /// <returns></returns>
        [HttpGet("Regex04")]
        public ActionResult<object> Regex04()
        {
            var msg = "";
            var text = "Hello, 안녕하세요.내 이름은 <범조> hello BeoBeaojojo 만나서 <반갑습니다> 다음에 또 만나요.";
            var regex = Regex.Match(text, @"\p{IsHangulSyllables}+");

            if (regex.Success)
            {
                msg += $"Index : {regex.Index}, value : {regex.Value}";
            }
            msg += "\r\n\r\n";

            text = "hello, 안녕하세요. My name is BeoBeomJojo 0424";
            regex = Regex.Match(text, @"[a-zA-Z0-9]+");
            if (regex.Success)
            {
                msg += $"Index : {regex.Index}, value : {regex.Value}";
            }

            msg += "\r\n\r\n";
            text = "afs65s 3a21fs432afdsa";
            regex = Regex.Match(text, @"[s?[0-9]+|a[0-9]]");
            if (regex.Success)
            {
                msg += $"Index : {regex.Index}, value : {regex.Value}";
            }

            // 클래스 삭제, src: 경로와 숫자찾기, style: 넓이 수정
            // <img class='fdfa' style='width:70%'> faaf <img clsass='tot' style='width:70%;'>
            //var text = " <img class='fdfa' style='width:70%'> afds<div class='kk'> faaf</div> <img clsass='tot' style='width:70%;'>";


            return msg;
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// MatchCollection mc = Regex.Matches(str, @"^강\w*구$");
        /// 메타문자  의미            
        /// ------------------------
        ///  ^        라인의 처음      
        ///  $        라인의 마지막    
        ///  \w       문자(영숫자) [a-zA-Z_0-9]
        ///  \s       Whitespace (공백,뉴라인,탭..)
        ///  \d       숫자
        ///  *        Zero 혹은 그 이상  
        /// +        하나 이상
        /// ?        Zero 혹은 하나
        /// .        Newline을 제외한 한 문자
        /// [ ]      가능한 문자들
        /// [^ ]     가능하지 않은 문자들
        /// [ - ]    가능 문자 범위
        /// {n,m}    최소 n개, 최대 m개
        /// (  )     그룹
        /// |        논리 OR
        /// 
        /// <returns></returns>
        [HttpGet("Regex05")]
        public ActionResult<object> Regex05()
        {
            var msg = "";
            string str = "   서울시 강남구 역삼동 강남아파트 1      ";
            //string patten = @"^\s+";      // 앞 공백제거
            string patten = @"^\s+|\s+$";        // 앞뒤 공벡제거
            Regex regex = new Regex(patten);

            msg = "__" + regex.Replace(str, "") + "__";
            msg += "\r\n\r\n";


            str = "02-632-5432; 032-645-7361";
            patten = @"(?<a_No>\d+)-(?<p_No>\d+-\d+)";
            regex = new Regex(patten);
            msg += regex.Replace(str, @"${p_No} (${a_No})");
            msg += "\r\n\r\n";

            str = "<img class='fdfa' style='width:72%'> afds<div class='kk'> faaf</div> <img class='tot' id='TAL' style='width: 70%;'>";
            //patten = @"<img[^>]*style\s*=\s*[""']?(?<style>[^""'>]+)[""']?";
            //patten = @"<img[^>]*class\s*=\s*[""']?(?<class>[^""'>]+)[""']?|style\s*=\s*[""']?(?<style>[^""'>]+)[""']?";
            //patten = @"(?=(style\s*=\s*[""']?(?<style>[^""'>]+)[""']?){1,})";

            //patten = @"(?=(width:\s*?(?<width>[^%]+)[%]?))";
            patten = @"<img[^>]*(?=(width:\s*(?<width>[^%]+)[%]?){1,})";
            var regex3 = Regex.Matches(str, patten);
            // <img class='fdfa' style='width:72%'
            string s_old = "";
            string s_new = "";
            foreach (Match item in regex3)
            {
                msg += "len::" + item.Groups.Count + ", 000::" + item.Groups[0].Value + ", 111::" + item.Groups[1].Value + ", 222::" + item.Groups[2].Value + "\r\n";
                string val = item.Groups[0].Value;
                var is_yn = val.IndexOf("tot") != -1 ? "ok": "no";
                if(is_yn == "ok")
                {
                    msg += "\r\n__" + item.Groups["width"].Value.Replace(item.Groups["width"].Value, "100") + "__\r\n";
                    s_old = string.Format("{0}{1}", item.Groups[0].Value, item.Groups[1].Value);
                    s_new = string.Format("{0}width: 100%", item.Groups[0].Value, item.Groups[1].Value);
                }
                msg += $"g 0 ={val}, SSS={item.Groups["style"].Value}, g CCC={item.Groups["width"].Value}..{is_yn}\r\n";
            }
            msg += "\r\n\r\n";
            msg += "\r\n\r\n";
            msg += s_old + "\r\n";
            msg += s_new + "\r\n";
            str = str.Replace(s_old, s_new);

            msg += "\r\n\r\n";

            patten = @"class\s*=\s*[""']?(?<class>[^""'>]+)[""']?";
            msg += Regex.Replace(str, patten, "");

            return msg;
        }

        [HttpGet("Regex06")]
        public ActionResult<object> Regex06()
        {
            string msg = "msg";
            return Ok(new {msg});
        }

    }
}

