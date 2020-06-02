﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonOldProgram
{
    public class CorporateSetting
    {
        private readonly string apiUrl = "https://example.com/2";
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        /// <summary>
        /// 連携ID 外部APIにて発行される
        /// </summary>
        public int CooperatedId { get; private set; }

        // APIへ設定情報を送り、その後に同様の情報をDBへ保存する
        public async Task Save()
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(this);
            var content = new StringContent(json, Encoding.UTF8, @"application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("API連携に失敗しました");
            }

            var cooperatedId = int.Parse(await response.Content.ReadAsStringAsync());

            await SaveDB(cooperatedId);
        }

        private static async Task SaveDB(int cooperatedId)
        {
            await Task.Run(() => { /*DBへ保存*/ });
        }

        public async Task ReadFromDB()
        {
            await Task.Run(() =>
            {
                Item1 = "設定情報1";
                Item2 = "設定情報2";
            });
        }
    }
}