using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocationArtController : MonoBehaviour
{
    public Player player;
    public Image sceneImage;
    public CanvasGroup sceneCanvasGroup;
    public RectTransform itemImageContainer;
    public Image itemImagePrefab;

    [Range(0.05f, 2f)]
    public float sceneFadeDuration = 0.35f;
    

    private Location displayedLocation;
    private Coroutine imageRoutine;
    private bool interactionImagePlaying;

    public void ShowLocation(Location location)
    {
        if (location == null)
            return;

        bool firstLocation = displayedLocation == null;
        bool locationChanged = displayedLocation != location;

        displayedLocation = location;

        if (!locationChanged)
        {
            if (!interactionImagePlaying)
                RefreshItemImages();

            return;
        }

        interactionImagePlaying = false;
        StopImageRoutine();

        if (firstLocation)
        {
            SetSceneImage(location.locationImage);
            RefreshItemImages();
            sceneCanvasGroup.alpha = 1f;
            return;
        }

        imageRoutine = StartCoroutine(
            ShowLocationSequence(location)
        );
    }

    public Coroutine PlayInteractionImage(Sprite image, float duration)
    {
        interactionImagePlaying = true;
        StopImageRoutine();

        imageRoutine = StartCoroutine(
            PlayInteractionSequence(image, duration)
        );

        return imageRoutine;
    }

    public void RefreshItemImages()
    {
        ClearItemImages();

        foreach (Item item in player.currentLocation.items)
        {
            if (!item.itemEnabled || item.locationSprite == null)
                continue;

            Image image = Instantiate(itemImagePrefab, itemImageContainer);
            image.name = item.itemName + "Image";
            image.sprite = item.locationSprite;
            image.preserveAspect = true;
            image.raycastTarget = false;

            RectTransform rect = image.rectTransform;
            rect.anchoredPosition = item.locationSpritePosition;
            rect.sizeDelta = item.locationSpriteSize;
            rect.localEulerAngles = new Vector3(
                0f,
                0f,
                item.locationSpriteRotation
            );
            rect.localScale = Vector3.one;
        }
    }

    private IEnumerator ShowLocationSequence(Location location)
    {
        yield return FadeScene(sceneCanvasGroup.alpha, 0f);

        SetSceneImage(location.locationImage);
        RefreshItemImages();

        yield return FadeScene(0f, 1f);

        imageRoutine = null;
    }

    private IEnumerator PlayInteractionSequence(Sprite image, float duration)
    {
        yield return FadeScene(sceneCanvasGroup.alpha, 0f);

        SetSceneImage(image);
        ClearItemImages();

        yield return FadeScene(0f, 1f);
        yield return new WaitForSeconds(Mathf.Max(0f, duration));
        yield return FadeScene(1f, 0f);

        SetSceneImage(player.currentLocation.locationImage);
        RefreshItemImages();

        yield return FadeScene(0f, 1f);

        interactionImagePlaying = false;
        imageRoutine = null;
    }

    private IEnumerator FadeScene(float startAlpha, float targetAlpha)
    {
        float elapsed = 0f;

        while (elapsed < sceneFadeDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / sceneFadeDuration);
            sceneCanvasGroup.alpha =
                Mathf.Lerp(startAlpha, targetAlpha, progress);

            yield return null;
        }

        sceneCanvasGroup.alpha = targetAlpha;
    }

    private void ClearItemImages()
    {
        for (int i = itemImageContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(itemImageContainer.GetChild(i).gameObject);
        }
    }

    private void StopImageRoutine()
    {
        if (imageRoutine == null)
            return;

        StopCoroutine(imageRoutine);
        imageRoutine = null;
    }

    private void SetSceneImage(Sprite sprite)
    {
        sceneImage.sprite = sprite;
        sceneImage.enabled = sprite != null;

        Color color = sceneImage.color;
        color.a = 1f;
        sceneImage.color = color;
    }
}