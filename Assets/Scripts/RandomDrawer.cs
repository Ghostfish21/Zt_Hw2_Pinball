using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RandomDrawer : MonoBehaviour {

    public GameObject dividerPrefab;
    public GameObject resultPrefab;
    public GameObject tmpPrefab;

    public float leftBoundX;
    public float rightBoundX;

    public List<RandomResultProfile> randomResultProfiles;
    
    // Start is called before the first frame update
    void Start() {

        float totalPercentage = 0;
        foreach (RandomResultProfile randomResultProfile in randomResultProfiles) 
            totalPercentage += randomResultProfile.percentage;
        foreach (RandomResultProfile randomResultProfile in randomResultProfiles) 
            randomResultProfile.percentage /= totalPercentage;

        float lastPercentageCursor = 0;
        float currentPercentageCursor = 0;
        for (int i = 0; i < randomResultProfiles.Count-1; i++) {
            RandomResultProfile rrp = randomResultProfiles[i];
            currentPercentageCursor += rrp.percentage;
            
            GameObject divider = Instantiate(dividerPrefab, transform);
            divider.transform.position = new Vector3(lerp(leftBoundX, rightBoundX, currentPercentageCursor), 
                -1.676613f, 0f);
            rrp.setDivider(divider);
            
            generateSingleResultArea(rrp, lastPercentageCursor, currentPercentageCursor);
            lastPercentageCursor = currentPercentageCursor;
        }
        RandomResultProfile rrp1 = randomResultProfiles[^1];
        currentPercentageCursor += rrp1.percentage;
        generateSingleResultArea(rrp1, lastPercentageCursor, currentPercentageCursor);
    }

    private void generateSingleResultArea(RandomResultProfile rrp, float lastCursor, float currentCursor) {
        GameObject result = Instantiate(resultPrefab, transform);
        result.GetComponent<SpriteRenderer>().color = rrp.color;
        result.transform.position = new Vector3(lerp(leftBoundX, rightBoundX, lerp(lastCursor, currentCursor, 0.5f)),
            -1.64f, 0f);
        result.transform.localScale = new Vector3(getWidth()*(currentCursor - lastCursor),
            result.transform.localScale.y, result.transform.localScale.z);
        result.GetComponent<RandomResult>().onHit = rrp.onHit;
        result.GetComponent<RandomResult>().shouldResetTime = rrp.shouldResetTime;
        rrp.setResult(result);

        GameObject tmp = Instantiate(tmpPrefab, transform);
        tmp.transform.position = new Vector3(lerp(leftBoundX, rightBoundX, lerp(lastCursor, currentCursor, 0.5f)),
            -1.64f, 0f);

        Vector2 sizeDelta = tmp.GetComponent<RectTransform>().sizeDelta;
        tmp.GetComponent<RectTransform>().sizeDelta = new Vector2(getWidth()*(currentCursor - lastCursor),
            sizeDelta.y);
        tmp.GetComponent<TMP_Text>().text = rrp.name;
        rrp.setTmp(tmp);
    }

    public float getWidth() {
        return Mathf.Abs(rightBoundX - leftBoundX);
    }

    public void update() {
        float lastPercentageCursor = 0;
        float currentPercentageCursor = 0;
        for (int i = 0; i < randomResultProfiles.Count-1; i++) {
            RandomResultProfile rrp = randomResultProfiles[i];
            currentPercentageCursor += rrp.percentage;
            
            rrp.getDivider().transform.position = new Vector3(lerp(leftBoundX, rightBoundX, currentPercentageCursor), 
                -1.676613f, 0f);
            
            updateSingleResultArea(rrp, lastPercentageCursor, currentPercentageCursor);
            lastPercentageCursor = currentPercentageCursor;
        }
        RandomResultProfile rrp1 = randomResultProfiles[^1];
        currentPercentageCursor += rrp1.percentage;
        updateSingleResultArea(rrp1, lastPercentageCursor, currentPercentageCursor);
    }
    
    private void updateSingleResultArea(RandomResultProfile rrp, float lastCursor, float currentCursor) {
        rrp.getResult().transform.position = new Vector3(lerp(leftBoundX, rightBoundX, lerp(lastCursor, currentCursor, 0.5f)),
            -1.64f, 0f);
        rrp.getResult().transform.localScale = new Vector3(getWidth()*(currentCursor - lastCursor),
            rrp.getResult().transform.localScale.y, rrp.getResult().transform.localScale.z);
        
        rrp.getTmp().transform.position = new Vector3(lerp(leftBoundX, rightBoundX, lerp(lastCursor, currentCursor, 0.5f)),
            -1.64f, 0f);

        Vector2 sizeDelta = rrp.getTmp().GetComponent<RectTransform>().sizeDelta;
        rrp.getTmp().GetComponent<RectTransform>().sizeDelta = new Vector2(getWidth()*(currentCursor - lastCursor),
            sizeDelta.y);
    }
    
    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
}

[Serializable]
public class RandomResultProfile {
    public string name;
    public float percentage;
    public Color color;
    public UnityEvent onHit;
    public bool shouldResetTime;
    private GameObject divider;
    public void setDivider(GameObject divider) {
        this.divider = divider;
    }
    public GameObject getDivider() {
        return divider;
    }
    private GameObject result;
    public void setResult(GameObject result) {
        this.result = result;
    }
    public GameObject getResult() {
        return result;
    }
    private GameObject tmp;
    public void setTmp(GameObject tmp) {
        this.tmp = tmp;
    }
    public GameObject getTmp() {
        return tmp;
    }
}
