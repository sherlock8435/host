namespace Model
{
    public class MailBox
    {
        public string SenderEmail { get; set; }
        public string msgDate { get; set; }
        public string SenderLname { get; set; }
        public string SenderFname { get; set; }
        public string msgSubject { get; set; }
        public string msgBody { get; set; }
        public bool msgRead { get; set; }
        public string RecieveEmail { get; set; }
    }
}
