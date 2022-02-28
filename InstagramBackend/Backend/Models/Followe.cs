namespace Backend.Models
{
    public class Follow
    {

        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        public AppUser Follower { get; set; }
        public AppUser Followee { get; set; }


    }
}
