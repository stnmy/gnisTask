using System;
using System.ComponentModel.DataAnnotations;

namespace gnisTask.Models
{
    public class MeetingMaster
    {
        public int MeetingId { get; set; }

        [Required(ErrorMessage = "Customer Type is required.")]
        public string CustomerType { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Meeting Date is required.")]
        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [Required(ErrorMessage = "Meeting Time is required.")]
        public TimeSpan MeetingTime { get; set; }

        [Required(ErrorMessage = "Meeting Place is required.")]
        public string MeetingPlace { get; set; }

        [Required(ErrorMessage = "Attends From Client Side is required.")]
        public string AttendsFromClientSide { get; set; }

        [Required(ErrorMessage = "Attends From Host Side is required.")]
        public string AttendsFromHostSide { get; set; }

        [Required(ErrorMessage = "Meeting Agenda is required.")]
        public string MeetingAgenda { get; set; }

        [Required(ErrorMessage = "Meeting Discussion is required.")]
        public string MeetingDiscussion { get; set; }

        [Required(ErrorMessage = "Meeting Decision is required.")]
        public string MeetingDecision { get; set; }
    }
}