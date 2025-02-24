using static System.Net.WebRequestMethods;

namespace Street.Healing.API.MailStyling
{
    public class HtmlMail
    {
        public string HtmlBody { get; set; }


        public HtmlMail(string otp)
        {
            HtmlBody = $@"
        <html>
            <head>
                <style>
                    body {{
                        font-family: 'Helvetica Neue', Arial, sans-serif;
                        background-color: #f9f0f4;
                        color: #4a4a4a;
                        margin: 0;
                        padding: 0;
                    }}

                    .email-wrapper {{
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        background-color: #f9f0f4;
                    }}

                    .email-content {{
                        width: 100%;
                        max-width: 650px;
                        background-color: #fff;
                        border-radius: 16px;
                        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
                        padding: 40px;
                        text-align: center;
                    }}

                    .email-header h1 {{
                        color: #d08099;
                        font-size: 36px;
                        font-weight: 700;
                        margin-bottom: 20px;
                        letter-spacing: 1px;
                    }}

                    .email-body p {{
                        color: #666;
                        font-size: 18px;
                        line-height: 1.6;
                        margin-bottom: 25px;
                        font-weight: 400;
                    }}

                    .otp-box {{
                        background-color: #f8c4d4;
                        padding: 20px 40px;
                        border-radius: 8px;
                        margin: 20px 0;
                        display: inline-block;
                        box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
                    }}

                    .otp {{
                        font-size: 32px;
                        font-weight: 700;
                        color: #fff;
                    }}

                    .email-footer {{
                        margin-top: 30px;
                        font-size: 14px;
                        color: #999;
                    }}

                    .email-footer a {{
                        color: #d08099;
                        text-decoration: none;
                        font-weight: bold;
                    }}

                    .email-footer a:hover {{
                        text-decoration: underline;
                    }}

                    @media screen and (max-width: 600px) {{
                        .email-content {{
                            padding: 20px;
                        }}
                        .email-header h1 {{
                            font-size: 28px;
                        }}
                        .otp-box {{
                            padding: 15px 30px;
                        }}
                        .otp {{
                            font-size: 28px;
                        }}
                    }}
                </style>
            </head>
            <body>
                <div class='email-wrapper'>
                    <div class='email-content'>
                        <div class='email-header'>
                            <h1>Welcome to Street Healing</h1>
                        </div>
                        <div class='email-body'>
                            <p>Thank you for choosing us! Your journey towards mental well-being starts here.</p>
                            <p>Your verification code is:</p>
                            <div class='otp-box'>
                                <span class='otp'>{otp}</span>
                            </div>
                            <p>We are here to support you every step of the way.</p>
                        </div>
                        <div class='email-footer'>
                            <p>Need help? <a href='mailto:support@healing.com'>Contact us</a></p>
                        </div>
                    </div>
                </div>
            </body>
        </html>";
        }
    }
}


