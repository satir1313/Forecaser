using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Speech.Synthesis;
using System.Threading;

namespace Forecaser
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.bg4;
            rdMand.Checked = true;
            rdMand.Anchor = AnchorStyles.None;
            rdMel.Anchor = AnchorStyles.None;
            rdCanb.Anchor = AnchorStyles.None;
            rdPerth.Anchor = AnchorStyles.None;
            rdSyd.Anchor = AnchorStyles.None;
        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        // decorate method with async word than the other async method vcan be waited : await setWeatherDetails(output);
        public async void getWeather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid=05a45c55bd90e88f5b9f247161828ed0&unit=metric", city);
                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<WeatheInfor.Root>(json);

                WeatheInfor.Root output = result;

                //lblCity.Text = string.Format("{0}", output.name);
                lblTemp.Text = string.Format("{0} \u00B0" + "C", Math.Round((output.main.temp - 273.15), 1));
                lblCountry.Text = string.Format("{0}", output.sys.country);

                string img = "http://openweathermap.org/img/wn/" + output.weather[0].icon + "@2x.png";
                pbWeather.ImageLocation = img;
                pbWeather.SizeMode = PictureBoxSizeMode.CenterImage;

                await setWeatherDetails(output);
            }

        }

        private void rdSyd_CheckedChanged(object sender, EventArgs e) {

            if(rdSyd.Checked == true) {
                getWeather("Sydney");
                getForcast(-33.840561, 151.207214);
            }
        }

        private void rdMel_CheckedChanged(object sender, EventArgs e) {

            if (rdMel.Checked == true) {
                getWeather("Melbourne");
                getForcast(-37.813999, 144.963318);
            }
        }

        private void rdPerth_CheckedChanged(object sender, EventArgs e) {

            if (rdPerth.Checked == true) {
                getWeather("Perth");
                // Perth lat and lon
                getForcast(-31.953512, 115.857048);
            }
        }

        private void pbWeather_Click(object sender, EventArgs e) {

        }

        private void rdCanb_CheckedChanged(object sender, EventArgs e) {

            if (rdCanb.Checked == true) {
                getWeather("Tehran");
                getForcast(35.694389, 51.421509);
            }
        }

        private void rdMand_CheckedChanged(object sender, EventArgs e) {

            if (rdMand.Checked == true) {
                getWeather("Mandurah");
                getForcast(-32.549999, 115.699997);
            }
        }
        private void label6_Click(object sender, EventArgs e) {

        }

        // change the method return type to Task then async can be asign to that and use await key work to keep this method wait for particular time
         async Task setWeatherDetails(WeatheInfor.Root output) {
            // keep this method waiting without blocking the main GUI to continue
            await Task.Delay(1500);
            lblDesc.Text = string.Format("{0}", output.weather[0].description);            
            lblhum.Text = string.Format("{0}", output.main.humidity + " %");
            lblWind.Text = string.Format("{0}", output.wind.speed + " km/hr");

            /*SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            if ((lblDesc.Text).Contains("cloud")) {
                
                speechSynthesizer.Speak("it's cloudy, Don't forget your brolly");
                speechSynthesizer.Dispose();
            }

            if ((lblDesc.Text).Contains("clear")) {
                speechSynthesizer.Speak("it's sunny, remember to use sun screen during the day");
                speechSynthesizer.Dispose();
            }

            if ((lblDesc.Text).Contains("heavy")) {
                speechSynthesizer.Speak("better to saty at home and have nice cup of coffee");
                speechSynthesizer.Dispose();
            }*/
        }

        public void getForcast(double lat, double lon) {
            string url = "";
            url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=hourly,minutely,current&appid=05a45c55bd90e88f5b9f247161828ed0", lat, lon);
            using (WebClient web = new WebClient()) {

                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<DailyForcast>(json);

                DailyForcast forcast = result;
                // add first day
                var date = convertDate(forcast.daily[0].dt);
                lblDay.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblTemp2.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[0].temp.day - 273.15),1) + " C");
                lblDesc2.Text = string.Format("{0}", forcast.daily[0].weather[0].description);
                string img = "http://openweathermap.org/img/wn/" + forcast.daily[0].weather[0].icon + "@2x.png";
                pbd1.ImageLocation = img;
                pbd1.SizeMode = PictureBoxSizeMode.CenterImage;

                // second day
                date = convertDate(forcast.daily[1].dt);
                lblD2.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblT2.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[1].temp.day - 273.15), 1) + " C");
                lblDe2.Text = string.Format("{0}", forcast.daily[1].weather[0].description);
                img = "http://openweathermap.org/img/wn/" + forcast.daily[1].weather[0].icon + "@2x.png";
                pbd2.ImageLocation = img;
                pbd2.SizeMode = PictureBoxSizeMode.CenterImage;

                // third day
                date = convertDate(forcast.daily[2].dt);
                lblD3.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblT3.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[2].temp.day - 273.15), 1) + " C");
                lblDe3.Text = string.Format("{0}", forcast.daily[2].weather[0].description);
                img = "http://openweathermap.org/img/wn/" + forcast.daily[2].weather[0].icon + "@2x.png";
                pbd3.ImageLocation = img;
                pbd3.SizeMode = PictureBoxSizeMode.CenterImage;

                // forth day
                date = convertDate(forcast.daily[3].dt);
                lblD4.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblT4.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[3].temp.day - 273.15), 1) + " C");
                lblDe4.Text = string.Format("{0}", forcast.daily[3].weather[0].description);
                img = "http://openweathermap.org/img/wn/" + forcast.daily[3].weather[0].icon + "@2x.png";
                pbd4.ImageLocation = img;
                pbd4.SizeMode = PictureBoxSizeMode.CenterImage;

                // fifth day
                date = convertDate(forcast.daily[4].dt);
                lblD5.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblT5.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[4].temp.day - 273.15), 1) + " C");
                lblDe5.Text = string.Format("{0}", forcast.daily[4].weather[0].description);
                img = "http://openweathermap.org/img/wn/" + forcast.daily[4].weather[0].icon + "@2x.png";
                pbd5.ImageLocation = img;
                pbd5.SizeMode = PictureBoxSizeMode.CenterImage;

                // sixth day
                date = convertDate(forcast.daily[5].dt);
                lblD6.Text = string.Format("{0}", date.ToString("dd/MM/yy"));
                lblT6.Text = string.Format("{0} \u00B0", Math.Round((forcast.daily[5].temp.day - 273.15), 1) + " C");
                lblDe6.Text = string.Format("{0}", forcast.daily[5].weather[0].description);
                img = "http://openweathermap.org/img/wn/" + forcast.daily[5].weather[0].icon + "@2x.png";
                pbd6.ImageLocation = img;
                pbd6.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }


        public DateTime convertDate(double time) {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(time).ToLocalTime().Date;
            return date;
        }

    }

}
