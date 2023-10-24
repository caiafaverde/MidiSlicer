//
//  NoteValue.swift
//  MusicTheory iOS
//
//  Created by Cem Olcay on 21.06.2018.
//  Copyright © 2018 cemolcay. All rights reserved.
//
//  https://github.com/cemolcay/MusicTheory
//

namespace MusicTheory
{

    // MARK: - NoteValueType

    /// Defines the types of note values.
    public enum NoteValueType : int //Int, Codable, CaseIterable, Hashable, CustomStringConvertible 
    {
        /// Four bar notes.
        FourBars,
        /// Two bar notes.
        TwoBars,
        /// One bar note.
        OneBar,
        /// Two whole notes.
        DoubleWhole,
        /// Whole note.
        Whole,
        /// Half note.
        Half,
        /// Quarter note.
        Quarter,
        /// Eighth note.
        Eighth,
        /// Sixteenth note.
        Sixteenth,
        /// Thirtysecond note.
        Thirtysecond,
        /// Sixtyfourth note.
        Sixtyfourth
    }

    public static class NoteValueExtensions
    {
        /// The note value's duration in beats.
        public static double Rate(this NoteValueType self)
        {
            switch (self)
            {
                case NoteValueType.FourBars: return 16.0 / 1.0;
                case NoteValueType.TwoBars: return 8.0 / 1.0;
                case NoteValueType.OneBar: return 4.0 / 1.0;
                case NoteValueType.DoubleWhole: return 2.0 / 1.0;
                case NoteValueType.Whole: return 1.0 / 1.0;
                case NoteValueType.Half: return 1.0 / 2.0;
                case NoteValueType.Quarter: return 1.0 / 4.0;
                case NoteValueType.Eighth: return 1.0 / 8.0;
                case NoteValueType.Sixteenth: return 1.0 / 16.0;
                case NoteValueType.Thirtysecond: return 1.0 / 32.0;
                case NoteValueType.Sixtyfourth: return 1.0 / 64.0;
            }
            return 0;
        }

        /// Returns the string representation of the note value type.
        public static string ToString(this NoteValueType self)
        {
            switch (self)
            {
                case NoteValueType.FourBars: return "4 Bars";
                case NoteValueType.TwoBars: return "2 Bars";
                case NoteValueType.OneBar: return "1 Bar";
                case NoteValueType.DoubleWhole: return "2/1";
                case NoteValueType.Whole: return "1/1";
                case NoteValueType.Half: return "1/2";
                case NoteValueType.Quarter: return "1/4";
                case NoteValueType.Eighth: return "1/8";
                case NoteValueType.Sixteenth: return "1/16";
                case NoteValueType.Thirtysecond: return "1/32";
                case NoteValueType.Sixtyfourth: return "1/64";
            }
            return string.Empty;
        }

        /// The string representation of the modifier.
        public static string ToString(this NoteModifier self)
        {
            switch (self)
            {
                //case NoteModifier.Default:
                default:
                    return "";
                case NoteModifier.Dotted: return "D";
                case NoteModifier.Triplet: return "T";
                case NoteModifier.Quintuplet: return "Q";
            }
        }

        public static double ToDouble(this NoteModifier self)
        {
            switch (self)
            {
                default:
                    return 0;
                case NoteModifier.Default:
                    return 1.0;
                case NoteModifier.Dotted: return 1.5;
                case NoteModifier.Triplet: return 0.6667;
                case NoteModifier.Quintuplet: return 0.8;
            }
        }
    }


    // MARK: - NoteModifier

    /// Defines the length of a `NoteValue`
    public enum NoteModifier //: Double, Codable, CaseIterable, CustomStringConvertible {
    {
        /// No additional length.
        Default, // = 1
        /// Adds half of its own value.
        Dotted, // = 1.5
        /// Three notes of the same value.
        Triplet, // = 0.6667
        /// Five of the indicated note value total the duration normally occupied by four.
        Quintuplet //= 0.8
    }

    /// Defines the duration of a note beatwise.
    public class NoteValue // : Codable, CustomStringConvertible
    {
        /// Type that represents the duration of note.
        public NoteValueType Type { get; set; }
        /// Modifier for `NoteType` that modifies the duration.
        public NoteModifier Modifier { get; set; }

        /// Initilize the NoteValue with its type and optional modifier.
        ///
        /// - Parameters:
        ///   - type: Type of note value that represents note duration.
        ///   - modifier: Modifier of note value. Defaults `default`.
        public NoteValue(NoteValueType type, NoteModifier modifier = NoteModifier.Default)
        {
            Type = type;
            Modifier = modifier;
        }

        /// Returns the string representation of the note value.
        public override string ToString()
        {
            return $"{Type}{Modifier}";
        }

        /// Calculates how many notes of a single `NoteValueType` is equivalent to a given `NoteValue`.
        ///
        /// - Parameters:
        ///   - noteValue: The note value to be measured.
        ///   - noteValueType: The note value type to measure the length of the note value.
        /// - Returns: Returns how many notes of a single `NoteValueType` is equivalent to a given `NoteValue`.
        public static double operator /(NoteValue noteValue, NoteValueType noteValueType)
        {
            return noteValue.Modifier.ToDouble() * noteValueType.Rate() / noteValue.Type.Rate();
        }
    }
}