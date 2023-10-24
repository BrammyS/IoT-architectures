using System.Text.Json.Serialization;

namespace IoT_Architectures.Api.Domain.Models;

public class SenML
{
    /// <summary>
    ///     The base name of a SenML pack states which device the measurements are from.
    ///     This should be unique!
    ///     https://docs.kpnthings.com/dm/concepts/senml#base-name-bn-required-in-pack
    /// </summary>
    /// <remarks>
    ///     Required in pack.
    /// </remarks>
    [JsonPropertyName("bn")]
    public string? BaseName { get; set; }

    /// <summary>
    ///     The timestamp of the measurement in UNIX.
    ///     If KPN Things receives a measurement without base time,
    ///     the time of receiving the measurement is taken as base time.
    ///     SenML being sent by KPN Things will always contain a base time.
    ///     https://docs.kpnthings.com/dm/concepts/senml#base-time-bt-optional-in-pack
    /// </summary>
    /// <remarks>
    ///     Required in pack.
    /// </remarks>
    [JsonPropertyName("bt")]
    public double? BaseTime { get; set; }

    /// <summary>
    ///     Name of the individual measurement.
    ///     https://docs.kpnthings.com/dm/concepts/senml#name-n-required-in-record
    /// </summary>
    /// <remarks>
    ///     Required in record.
    /// </remarks>
    [JsonPropertyName("n")]
    public string? Name { get; set; }

    /// <summary>
    ///     Unit of the measurement. Write your u-values in camel case, so start with a lower case character.
    ///     https://docs.kpnthings.com/dm/concepts/senml#unit-u-optional-in-record
    /// </summary>
    /// <remarks>
    ///     Optional in record.
    /// </remarks>
    [JsonPropertyName("u")]
    public string? Unit { get; set; }

    /// <summary>
    ///     The timestamp of an individual record relative to the base time in seconds.
    ///     https://docs.kpnthings.com/dm/concepts/senml#time-t-optional-in-record
    /// </summary>
    /// <remarks>
    ///     Optional in record.
    /// </remarks>
    [JsonPropertyName("t")]
    public long? Time { get; set; }

    /// <summary>
    ///     Value of the number measurement.
    ///     https://docs.kpnthings.com/dm/concepts/senml#number-value-v-required-in-number-record
    /// </summary>
    /// <remarks>
    ///     Optional in record.
    /// </remarks>
    [JsonPropertyName("v")]
    public double? Number { get; set; }

    /// <summary>
    ///     Boolean value for a boolean measurement. Should be true or false.
    ///     https://docs.kpnthings.com/dm/concepts/senml#boolean-value-vb-required-in-boolean-record
    /// </summary>
    /// <remarks>
    ///     Optional in record.
    /// </remarks>
    [JsonPropertyName("vb")]
    public bool? Boolean { get; set; }

    /// <summary>
    ///     String value for a string measurement.
    ///     https://docs.kpnthings.com/dm/concepts/senml#string-value-vs-required-in-string-record
    /// </summary>
    /// <remarks>
    ///     Optional in record.
    /// </remarks>
    [JsonPropertyName("vs")]
    public string? String { get; set; }

    /// <summary>
    ///     Data value for a data measurement. Should be a base64 encoded string.
    ///     https://docs.kpnthings.com/dm/concepts/senml#data-value-vd-required-in-data-record
    /// </summary>
    [JsonPropertyName("vd")]
    public string? Data { get; set; }

    public bool IsPack()
    {
        return BaseName != null && BaseTime != null;
    }

    public bool IsRecord()
    {
        return Name != null;
    }
}