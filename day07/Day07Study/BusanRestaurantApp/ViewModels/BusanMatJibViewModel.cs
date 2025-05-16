using BusanRestaurantApp.Helpers;
using BusanRestaurantApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusanRestaurantApp.ViewModels
{
    public partial class BusanMatJibViewModel : ObservableObject
    {
        IDialogCoordinator dialogCoordinator;
        private ObservableCollection<BusanItem> _busanItems;

        public BusanMatJibViewModel(IDialogCoordinator coordinator)
        {
            dialogCoordinator = coordinator;

            GetDataFromOpenApi();
        }
        ObservableCollection<BusanItem>BusanItems { 
            get => _busanItems; 
            set => SetProperty(ref _busanItems, value); }

        private async Task GetDataFromOpenApi()
        {
            string baseUri = "http://apis.data.go.kr/6260000/FoodService/getFoodKr";
            string myServiceKey = "YWInX0dS13rB5yKLhl%2Bt5IjAXipcTUQFDpHkN1to%2BD3aE3XUK6LkcSmyAQh1rVnCDhxvF45A40%2B7SmgrQ%2BNn%2Bw%3D%3D";
            StringBuilder strUri = new StringBuilder();
            strUri.Append($"serviceKey={myServiceKey}");
            strUri.Append($"pageNo={1}&");
            strUri.Append($"numOfRows={3}&");
            strUri.Append($"resultType=json");
            string totalOpenApi = $"{baseUri}?{strUri}";
            Common.LOGGER.Info(totalOpenApi);

            HttpClient client = new HttpClient();
            ObservableCollection<BusanItem> busanItems = new ObservableCollection<BusanItem>();

            try
            {
                var response = await client.GetStringAsync(totalOpenApi);
                Common.LOGGER.Info(response);

                // Newtonsoft.Json으로 Json처리방식
                var jsonResut = JObject.Parse(response);
                var message = jsonResut["getFoodKr"]["header"]["message"];
                //await this.dialogCoordinator.ShowMessageAsync(this, "결과메시지", message.ToString());
                var status = Convert.ToString(jsonResut["getFoodKr"]["header"]["message"]); // 00이면 성공!

                if(status=="00")
                {
                    var item = jsonResut["getFoodKr"]["item"];
                    var jsonArray = item as JArray;
                    foreach (var subitem in jsonArray)
                    {
                        //Common.LOGGER.Info(subitem.ToString()); 
                        busanItems.Add(new BusanItem {
                            Uc_Seq = Convert.ToInt32(subitem["UC_SEQ"]),
                            Main_Title = Convert.ToString(subitem["MAIN_TITLE"]),
                            Gugun_Nm = Convert.ToString(subitem["GUGUN_NM"]),
                            Lat = Convert.ToDouble(subitem["LAT"]),
                            Lng = Convert.ToDouble(subitem["LNG"]),
                            Place = Convert.ToString(subitem["PLACE"]),
                            Title = Convert.ToString(subitem["TITLE"]),
                            SubTitle = Convert.ToString(subitem["SUBTITLE"]),
                            Addr1 = Convert.ToString(subitem["ADDR1"]),
                            Addr2 = Convert.ToString(subitem["ADDR2"]),
                            Cntct_Tel = Convert.ToString(subitem["CNTCT_TEL"]),
                            Homepage_Url = Convert.ToString(subitem["HOMEPAGE_URL"]),
                            Usage_Day_Week_And_Time = Convert.ToString(subitem["USAGE_DAY_WEEK_AND_TIME"]),
                            Rprsntv_Menu = Convert.ToString(subitem["RPRSNTV_MENU"]),
                            Main_Img_Normal = Convert.ToString(subitem["MAIN_IMG_NORMAL"]),
                            Main_Img_Thumb = Convert.ToString(subitem["MAIN_IMG_THUMB"]),
                            ItemCntnts = Convert.ToString(subitem["ITEMCNTNTS"]),
                        });
                    }
                    BusanItems = busanItems;
                }
            }
            catch (Exception ex)
            {
                await this.dialogCoordinator.ShowMessageAsync(this, "예외", ex.Message);
                Common.LOGGER.Fatal(ex.Message);
            }
        }
    }
}
