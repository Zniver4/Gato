using UnityEngine;
public class RoomSelect : MonoBehaviour
{
    public GameObject rooms;
    public GameObject anime;
    public GameObject movies;

    public void Anime()
    {
        Canvas room = rooms.GetComponent<Canvas>();
        room.enabled = false;
        
        Canvas anim = anime.GetComponent<Canvas>();
        anim.enabled = true;
    }

    public void Movies()
    {
        Canvas room = rooms.GetComponent<Canvas>();
        room.enabled = false;
        
        Canvas movie = movies.GetComponent<Canvas>();
        movie.enabled = true;
    }

    public void BackAnime()
    {
        Canvas room = rooms.GetComponent<Canvas>();
        room.enabled = true;
        
        Canvas anim = anime.GetComponent<Canvas>();
        anim.enabled = false;
    }
    
    public void BackMovies()
    {
        Canvas room = rooms.GetComponent<Canvas>();
        room.enabled = true;
        
        Canvas movie = movies.GetComponent<Canvas>();
        movie.enabled = false;
    }
}
