using UnityEngine;
using System;
using System.Collections.Generic;

public enum ResponseCurveType {
    Constant,
    Polynomial,
    Logistic,
    Logit,
    Threshold,
    Quadratic,
    Parabolic,
    NormalDistribution,
    Bounce,
    Sin
}

[Serializable]
public class ResponseCurve : ICloneable {
    public delegate float ResponseCurveCallbackType(float input);

    public const string Polynomial = "Polynomial";
    public const string InversePolynomial = "InversePolynomial";
    public const string Logarithmic = "Logarithmic";
    public const string Logit = "Logit";
    public const string Threshold = "Threshold";

    private ResponseCurveType _curveType;
    public ResponseCurveType curveType {
        get {
            return curveType;
        }
        set {
            _curveType = value;
        }
    }

    public float slope; //(m)
    public float exp; //(k)
    public float vShift; //vertical shift (b)
    public float hShift; //horizonal shift (c)
    public float threshold;
    public bool invert;

    private ResponseCurveCallbackType responseCurveCallback;

    public ResponseCurve() {
        curveType = ResponseCurveType.Polynomial;
        slope = 1;
        exp = 1;
        vShift = 0;
        hShift = 0;
        threshold = 0;
        invert = false;
    }

    void SwitchCurveType() {
        switch (curveType) {
            case ResponseCurveType.Constant:
                responseCurveCallback = _Constant;
                break;
            case ResponseCurveType.Polynomial:
                responseCurveCallback = _Polynomial;
                break;
            case ResponseCurveType.Logistic:
                responseCurveCallback = _Logistic;
                break;
            case ResponseCurveType.Logit:
                responseCurveCallback = _Logit;
                break;
            case ResponseCurveType.Quadratic:
                responseCurveCallback = _Quadratic;
                break;
            case ResponseCurveType.Sin:
                responseCurveCallback = _Sin;
                break;
            case ResponseCurveType.Parabolic:
                responseCurveCallback = _Parabolic;
                break;
            case ResponseCurveType.Bounce:
                responseCurveCallback = _Bounce;
                break;
            case ResponseCurveType.NormalDistribution:
                responseCurveCallback = _NormalDistribution;
                break;
            case ResponseCurveType.Threshold:
                responseCurveCallback = _Threshold;
                break;
            default:
                throw new Exception(curveType + " curve has not been implemented yet");
        }
    }

    private float _Constant(float input) {
        return threshold;
    }

    private float _Polynomial(float input) {
        return slope * (Mathf.Pow((input - hShift), exp)) + vShift;
    }

    private float _Logistic(float input) {
        return (exp * (1.0f / (1.0f + Mathf.Pow(Mathf.Abs(1000.0f * slope), (-1.0f * input) + hShift + 0.5f)))) + vShift;
    }

    private float _Logit(float input) {
        return (-Mathf.Log((1.0f / Mathf.Pow(Mathf.Abs(input - hShift), exp)) - 1.0f) * 0.05f * slope) + (0.5f + vShift);
    }

    private float _Quadratic(float input) {
        return ((slope * input) * Mathf.Pow(Mathf.Abs(input + hShift), exp)) + vShift;
    }

    private float _Sin(float input) {
        return (Mathf.Sin((2 * Mathf.PI * slope) * Mathf.Pow(input + (hShift - 0.5f), exp)) * 0.5f) + vShift + 0.5f;
    }

    private float _Parabolic(float input) {
        return Mathf.Pow(slope * (input + hShift), 2) + (exp * (input + hShift)) + vShift;
    }

    private float _Bounce(float input) {
        return Mathf.Abs(Mathf.Sin((2f * Mathf.PI * exp) * (input + hShift + 1f) * (input + hShift + 1f)) * (1f - input) * slope) + vShift;
    }

    private float _NormalDistribution(float input) {
        return (exp / (Mathf.Sqrt(2 * Mathf.PI))) * Mathf.Pow(2.0f, (-(1.0f / (Mathf.Abs(slope) * 0.01f)) * Mathf.Pow(input - (hShift + 0.5f), 2.0f))) + vShift;
    }

    private float _Threshold(float input) {
        return input > hShift ? (1.0f - vShift) : (0.0f - (1.0f - slope));
    }

    public float Evaluate(float input) {
        input = Mathf.Clamp01(input);
        float output = 0;
        if (input < threshold && curveType != ResponseCurveType.Constant) return 0;
        output = responseCurveCallback(input);
        if (invert) output = 1f - output;
        return Mathf.Clamp01(output);
    }


    public float _Evaluate(float input) {
        input = Mathf.Clamp01(input);
        float output = 0;
        if (input < threshold && curveType != ResponseCurveType.Constant) return 0;
        switch (curveType) {
            case ResponseCurveType.Constant:
                output = threshold;
                break;
            case ResponseCurveType.Polynomial: // y = m(x - c)^k + b 
                output = slope * (Mathf.Pow((input - hShift), exp)) + vShift;
                break;
            case ResponseCurveType.Logistic: // y = (k * (1 / (1 + (1000m^-1*x + c))) + b
                output = (exp * (1.0f / (1.0f + Mathf.Pow(Mathf.Abs(1000.0f * slope), (-1.0f * input) + hShift + 0.5f)))) + vShift; // Note, addition of 0.5 to keep default 0 hShift sane
                break;
            case ResponseCurveType.Logit: // y = -log(1 / (x + c)^K - 1) * m + b
                output = (-Mathf.Log((1.0f / Mathf.Pow(Mathf.Abs(input - hShift), exp)) - 1.0f) * 0.05f * slope) + (0.5f + vShift); // Note, addition of 0.5f to keep default 0 XIntercept sane
                break;
            case ResponseCurveType.Quadratic: // y = mx * (x - c)^K + b
                output = ((slope * input) * Mathf.Pow(Mathf.Abs(input + hShift), exp)) + vShift;
                break;
            case ResponseCurveType.Sin: //sin(m * (x + c) ^ K + b
                output = (Mathf.Sin((2 * Mathf.PI * slope) * Mathf.Pow(input + (hShift - 0.5f), exp)) * 0.5f) + vShift + 0.5f;
                break;
            case ResponseCurveType.Parabolic:
                output = Mathf.Pow(slope * (input + hShift), 2) + (exp * (input + hShift)) + vShift;
                break;
            case ResponseCurveType.Bounce:
                output = Mathf.Abs(Mathf.Sin((2f * Mathf.PI * exp) * (input + hShift + 1f) * (input + hShift + 1f)) * (1f - input) * slope) + vShift;
                break;
            case ResponseCurveType.NormalDistribution: // y = K / sqrt(2 * PI) * 2^-(1/m * (x - c)^2) + b
                output = (exp / (Mathf.Sqrt(2 * Mathf.PI))) * Mathf.Pow(2.0f, (-(1.0f / (Mathf.Abs(slope) * 0.01f)) * Mathf.Pow(input - (hShift + 0.5f), 2.0f))) + vShift;
                break;
            case ResponseCurveType.Threshold:
                output = input > hShift ? (1.0f - vShift) : (0.0f - (1.0f - slope));
                break;
            default:
                throw new Exception(curveType + " curve has not been implemented yet");
        }
        if (invert) output = 1f - output;
        return Mathf.Clamp01(output);
    }

    public void Reset() {
        slope = 1;
        exp = 1;
        vShift = 0;
        hShift = 0;
        threshold = 0;
        invert = false;
    }

    public string DisplayString {
        get { return " slope: " + slope + " exp: " + exp + " vShift: " + vShift + " hShift: " + hShift + " \n threshold: " + threshold + " inverted: " + invert; }
    }

    public override string ToString() {
        return "{type: " + curveType + ", slope: " + slope +
            ", exp: " + exp + ", vShift: " + vShift + ", hShift: " + hShift + "}";
    }

    public object Clone() {
        ResponseCurve curve = new ResponseCurve();
        curve.slope = slope;
        curve.exp = exp;
        curve.vShift = vShift;
        curve.hShift = hShift;
        curve.threshold = threshold;
        curve.curveType = curveType;
        return curve;
    }
}
