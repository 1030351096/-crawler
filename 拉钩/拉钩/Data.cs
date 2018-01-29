using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Model;
using DripOldDriver;
using HtmlAgilityPack;
using System.Threading;
using System.Diagnostics;

namespace 拉钩
{
    public class Data
    {
        private static Stopwatch stopwatch = new Stopwatch();

        public static void Get_data(int page)
        {
            stopwatch.Start();
            main(page);
            Console.ReadKey();

        }

        private static async void main(int page)
        {
            var client = new RestClient("https://www.lagou.com/jobs/positionAjax.json?needAddtionalResult=false&isSchoolJob=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "90acdd78-5daa-42b1-48f5-e1cc174c218e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("referer", "https://www.lagou.com/jobs/list_.net?labelWords=&fromSearch=true&suginput=");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddCookie("JSESSIONID", "ABAAABAAAFCAAEG9A2DF24523FAB7D1FD63A005976F3F74");
            request.AddCookie("user_trace_token", "20180124132833-9a3981e6-93bb-4036-9b6f-2ae37f2420b6");

            request.AddCookie("X_HTTP_TOKEN", "134fc7ed6f25d6a9b3448602e67d3107");
            request.AddCookie("_ga", "GA1.2.1335658696.1516771714");
            request.AddCookie("_gid", "GA1.2.1369058252.1516771714");
            request.AddCookie("PRE_UTM", "");
            request.AddCookie("LGUID", "20180124132834-6bba0edf-00c7-11e8-a7a9-5254005c3644");
            request.AddCookie("Hm_lvt_4233e74dff0ae5bd0a3d81c6ccf756e6", "1516771714");
            request.AddCookie("LGSID", "20180124132928-8c4b6c12-00c7-11e8-a7a9-5254005c3644");
            request.AddCookie("PRE_HOST", "www.baidu.com");
            request.AddCookie("PRE_SITE", "https%3A%2F%2Fwww.baidu.com%2Flink%3Furl%3D9AwEcKK5O6x-nq8-qhErKbTRwXXEivUmcP87cJqf8lW%26wd%3D%26eqid%3Dab5df59b0000da04000000025a6819b3");
            request.AddCookie("PRE_LAND", "https%3A%2F%2Fwww.lagou.com%2F");
            request.AddCookie("index_location_city", "%E5%85%A8%E5%9B%BD");
            request.AddCookie("_gat", "1");
            request.AddCookie("LGRID", "20180124132938-921fd830-00c7-11e8-8880-525400f775ce");
            request.AddCookie("Hm_lpvt_4233e74dff0ae5bd0a3d81c6ccf756e6", "1516771779");
            request.AddCookie("TG-TRACK-CODE", "search_code");
            request.AddCookie("SEARCH_ID", "8768959c73ea4804a3a5105e61f64c85");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"pn\"\r\n\r\n" + page + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"kd\"\r\n\r\n.net\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);



            IRestResponse response = client.Execute(request);

            await Task.Run(() => NewMethod(page, response));
        }

        private static void NewMethod(int page, IRestResponse response)
        {
            dynamic json = JsonConvert.DeserializeObject<dynamic>(response.Content);
            if (json.content.positionResult.result != null)
            {
                foreach (dynamic item in json.content.positionResult.result)
                {
                    Lagou lagou = new Lagou();
                    lagou.companyId = item.companyId;
                    lagou.formatCreateTime = item.formatCreateTime;
                    lagou.score = item.score;
                    lagou.positionId = item.positionId;
                    if (!SqlHelper.is_Insert(lagou.positionId.Value))
                    {
                        continue;
                    }
                    lagou.positionName = item.positionName;
                    lagou.createTime = item.createTime;
                    lagou.positionAdvantage = item.positionAdvantage;
                    lagou.salary = item.salary;
                    lagou.workYear = item.workYear;
                    lagou.education = item.education;
                    lagou.city = item.city;
                    lagou.companyLogo = item.companyLogo;
                    lagou.jobNature = item.jobNature;
                    lagou.approve = item.approve;
                    lagou.industryField = item.industryField;
                    lagou.companyShortName = item.companyShortName;
                    lagou.financeStage = item.financeStage;
                    lagou.companySize = item.companySize;


                    //lagou.positionLables = item.positionLables;
                    //lagou.industryLables = item.industryLables;
                    lagou.publisherId = item.publisherId;
                    //lagou.companyLabelList = item.companyLabelList;
                    lagou.district = item.district;
                    //lagou.businessZones = item.businessZones;
                    lagou.imState = item.imState;
                    lagou.lastLogin = item.lastLogin;
                    lagou.explain = item.explain;
                    lagou.plus = item.plus;
                    lagou.pcShow = item.pcShow;
                    lagou.appShow = item.appShow;
                    lagou.deliver = item.deliver;
                    lagou.gradeDescription = item.gradeDescription;
                    lagou.promotionScoreExplain = item.promotionScoreExplain;
                    lagou.firstType = item.firstType;
                    lagou.secondType = item.secondType;
                    lagou.isSchoolJob = item.isSchoolJob;
                    lagou.companyFullName = item.companyFullName;
                    Console.WriteLine("已获取到" + lagou.companyFullName + "招聘信息");
                    lagou.adWord = item.adWord;
                    int id = 0;
                    bool is_insert = SqlHelper.Insert(lagou, ref id);
                    Console.WriteLine(is_insert == true ? "添加成功" : "添加失败");
                    if (lagou.positionId.HasValue)
                    {
                        Thread.Sleep(2000);
                        data_info(lagou.positionId.Value, id);
                        Console.WriteLine("修改信息成功!");
                    }
                }
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                Console.WriteLine($"已添加完此页,当前页码数位{page}");
                Thread.Sleep(5000);
                Get_data(page = page + 1);
                
            }
            else
            {
                Console.WriteLine("出现异常,暂停记录5秒");
                Thread.Sleep(5000);
                Get_data(page = page + 1);
            }

        }

        public static void data_info(int positionId, int id)
        {
            var httpResult = new HttpHelper().GetHtml(new HttpItem()
            {
                URL = "https://www.lagou.com/jobs/" + positionId + ".html?source=position_rec&i=position_rec-4"
            });
            if (httpResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HtmlDocument hd = new HtmlDocument();
                hd.LoadHtml(httpResult.Html);
                var body = hd.DocumentNode.SelectSingleNode("//*[@id='job_detail']");
                var title = body.SelectSingleNode("./dd[1]").InnerText.Trim(); //职位诱惑：
                var context = body.SelectSingleNode("./dd[2]/div").InnerText.Trim(); //职位描述：
                var work = body.SelectSingleNode("./dd[3]/div[1]").InnerText.Replace("查看地图", "").Trim(); //工作地址
                //var pubisher = body.SelectSingleNode("./dd[4]/div/div[1]").InnerText; //职位发布者  
                //var data_title = body.SelectSingleNode("./dd[4]/div/div[2]/div[1]/span[3]").InnerText;//聊天意愿
                var tip = body.SelectSingleNode("./dd[4]/div/div[2]/div[1]/span[4]").InnerText.Replace("回复率--&nbsp;", "");//回复率
                var ti = body.SelectSingleNode("./dd[4]/div/div[2]/div[2]/span[3]").InnerText;//简历处理
                var chu = body.SelectSingleNode("./dd[4]/div/div[2]/div[2]/span[4]").InnerText.Replace("处理率100%&nbsp;", "");//处理率
                var huo = body.SelectSingleNode("./dd[4]/div/div[2]/div[3]/span[3]").InnerText;//活跃时段
                var huotime = body.SelectSingleNode("./dd[4]/div/div[2]/div[3]/span[4]").InnerText;//活跃小时
                SqlHelper.update(id, title, context, work);
            }
            else
            {
                Console.WriteLine("获取详情失败" + httpResult.StatusCode + "");
            }
        }


    }
}
