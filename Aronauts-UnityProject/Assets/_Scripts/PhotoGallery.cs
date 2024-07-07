using UnityEngine;

public class PhotoGallery : MonoBehaviour
{
    public GameObject[] photos; // Assign your photo game objects in the inspector
    private int currentIndex = 0; // Start with the first photo

    private void Start()
    {
        UpdateGallery();
    }

    public void NextPhoto()
    {
        // Increase currentIndex and wrap around if necessary
        currentIndex = (currentIndex + 1) % photos.Length;
        UpdateGallery();
    }

    public void PreviousPhoto()
    {
        // Decrease currentIndex and wrap around if necessary
        currentIndex--;
        if (currentIndex < 0) currentIndex = photos.Length - 1;
        UpdateGallery();
    }

    private void UpdateGallery()
    {
        // Deactivate all photos
        foreach (var photo in photos)
        {
            photo.SetActive(false);
        }

        // Activate the current photo
        photos[currentIndex].SetActive(true);
    }
}
