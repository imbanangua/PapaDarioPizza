using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PapaDarioPizza
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutUs : Page
    {
        public AboutUs()
        {
            this.InitializeComponent();

            textAboutUs.Text = "        As early as 1976, Papa Dario was a young ballet dancer. " +
                "Having practiced ballet since he was a child, " +
                "he finally had the opportunity to perform in countries all over the world this year. " +
                "In a performance in Italy, " +
                "Papa Dario saw a child in the front row excitedly waving his pizza in the curtain call. " +
                "At that time, Papa Dario had not tasted this thin cracker-like pizza, " +
                "and it was rare in his hometown. " +
                "After the performance, Papa Dario went to buy a piece to taste. Surprisingly, " +
                "he was so in love with the pizza. After returning to his hometown, " +
                "Papa Dario resolutely decided to spread the delicacy in his hometown, " +
                "so he began to study cooking pizza. A few years passed, " +
                "Papa Dario’s Pizza was established. " +
                "Papa Dario also gave up his original identity as a ballet dancer and began to spread this delicacy to all over the world.";

            textContact.Text = "Email: PapaDarioPizza@gmail.com\n\nPhone number: 888-888-8888";
        
        }
    }
}
