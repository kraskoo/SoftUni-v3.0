namespace P13ContactUsForm
{
    using System;
    using Properties;

    public class ContactUsForm
    {
        public static void Main()
        {
            var newHtml = Resources.HTML.Replace(
                "<link rel=\"stylesheet\" href=\"styles/contact-us-form.css\" />",
                $"<style>{Resources.contact_us_form}</style>");
            Console.WriteLine(newHtml);
        }
    }
}