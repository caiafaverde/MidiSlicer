//
//  Interval.swift
//  MusicTheory
//
//  Created by Cem Olcay on 22.06.2018.
//  Copyright © 2018 cemolcay. All rights reserved.
//
//  https://github.com/cemolcay/MusicTheory
//

namespace MusicTheory
{

    public static class Extensions
    {
        /// Returns the notation of the interval quality.
        public static string Notation(this Interval.QualityType self)
        {
            switch (self)
            {
                case Interval.QualityType.Diminished: return "d";
                case Interval.QualityType.Perfect: return "P";
                case Interval.QualityType.Minor: return "m";
                case Interval.QualityType.Major: return "M";
                case Interval.QualityType.Augmented: return "A";
            }
            return string.Empty;
        }


    }

    /// Checks the equality of two `Interval`s in terms of their semitones.
    ///
    /// - Parameters:
    ///   - lhs: Left hand side of the equation.
    ///   - rhs: Right hand side of the equation.
    /// - Returns: Returns true if two `Interval`s are equal.
    // public func == (lhs: Interval, rhs: Interval) -> Bool {
    //return lhs.semitones == rhs.semitones
    //}

    /// Checks the equality of two `Interval`s in terms of their quality, degree and semitones.
    ///
    /// - Parameters:
    ///   - lhs: Left hand side of the equation.
    ///   - rhs: Right hand side of the equation.
    /// - Returns: Returns true if two `Interval`s are equal.
    ///public func === (lhs: Interval, rhs: Interval) -> Bool {
    //  return lhs.quality == rhs.quality && rhs.degree == rhs.degree && lhs.semitones == rhs.semitones
    //}

    /// Defines the interval between `Pitch`es in semitones.
    public class Interval//: Codable, Hashable, CustomStringConvertible
    {
        /// Quality type of the interval.
        public enum QualityType : int//, Codable, Hashable, CaseIterable, CustomStringConvertible
        {
            Diminished,
            Perfect,
            Minor,
            Major,
            Augmented
        }

        /// Quality of the interval.
        public QualityType Quality { get; set; }
        /// Degree of the interval.
        public int Degree { get; set; }
        /// Semitones interval affect on a pitch.
        public int Semitones { get; set; }

        /// Initilizes the interval with its quality, degree and semitones.
        ///
        /// - Parameters:
        ///   - quality: Quality of the interval.
        ///   - degree: Degree of the interval.
        ///   - semitones: Semitones of the interval.
        public Interval(QualityType quality, int degree, int semitones)
        {
            Quality = quality;
            Degree = degree;
            Semitones = semitones;
        }

        /// Unison.
        public static Interval P1 = new Interval(QualityType.Perfect, 1, 0);
        /// Perfect fourth.
        public static Interval P4 = new Interval(QualityType.Perfect, 4, 5);
        /// Perfect fifth.
        public static Interval P5 = new Interval(QualityType.Perfect, 5, 7);
        /// Octave.
        public static Interval P8 = new Interval(QualityType.Perfect, 8, 12);
        /// Perfect eleventh.
        public static Interval P11 = new Interval(QualityType.Perfect, 11, 17);
        /// Perfect twelfth.
        public static Interval P12 = new Interval(QualityType.Perfect, 12, 19);
        /// Perfect fifteenth, double octave.
        public static Interval P15 = new Interval(QualityType.Perfect, 15, 24);

        /// Minor second.
        public static Interval m2 = new Interval(QualityType.Minor, degree: 2, semitones: 1);
        /// Minor third.
        public static Interval m3 = new Interval(QualityType.Minor, degree: 3, semitones: 3);
        /// Minor sixth.
        public static Interval m6 = new Interval(QualityType.Minor, degree: 6, semitones: 8);
        /// Minor seventh.
        public static Interval m7 = new Interval(QualityType.Minor, degree: 7, semitones: 10);
        /// Minor ninth.
        public static Interval m9 = new Interval(QualityType.Minor, degree: 9, semitones: 13);
        /// Minor tenth.
        public static Interval m10 = new Interval(QualityType.Minor, degree: 10, semitones: 15);
        /// Minor thirteenth.
        public static Interval m13 = new Interval(QualityType.Minor, degree: 13, semitones: 20);
        /// Minor fourteenth.
        public static Interval m14 = new Interval(QualityType.Minor, degree: 14, semitones: 22);

        /// Major second.
        public static Interval M2 = new Interval(QualityType.Major, degree: 2, semitones: 2);
        /// Major third.
        public static Interval M3 = new Interval(QualityType.Major, degree: 3, semitones: 4);
        /// Major sixth.
        public static Interval M6 = new Interval(QualityType.Major, degree: 6, semitones: 9);
        /// Major seventh.
        public static Interval M7 = new Interval(QualityType.Major, degree: 7, semitones: 11);
        /// Major ninth.
        public static Interval M9 = new Interval(QualityType.Major, degree: 9, semitones: 14);
        /// Major tenth.
        public static Interval M10 = new Interval(QualityType.Major, degree: 10, semitones: 16);
        /// Major thirteenth.
        public static Interval M13 = new Interval(QualityType.Major, degree: 13, semitones: 21);
        /// Major fourteenth.
        public static Interval M14 = new Interval(QualityType.Major, degree: 14, semitones: 23);

        /// Diminished first.
        public static Interval d1 = new Interval(QualityType.Diminished, degree: 1, semitones: -1);
        /// Diminished second.
        public static Interval d2 = new Interval(QualityType.Diminished, degree: 2, semitones: 0);
        /// Diminished third.
        public static Interval d3 = new Interval(QualityType.Diminished, degree: 3, semitones: 2);
        /// Diminished fourth.
        public static Interval d4 = new Interval(QualityType.Diminished, degree: 4, semitones: 4);
        /// Diminished fifth.
        public static Interval d5 = new Interval(QualityType.Diminished, degree: 5, semitones: 6);
        /// Diminished sixth.
        public static Interval d6 = new Interval(QualityType.Diminished, degree: 6, semitones: 7);
        /// Diminished seventh.
        public static Interval d7 = new Interval(QualityType.Diminished, degree: 7, semitones: 9);
        /// Diminished eighth.
        public static Interval d8 = new Interval(QualityType.Diminished, degree: 8, semitones: 11);
        /// Diminished ninth.
        public static Interval d9 = new Interval(QualityType.Diminished, degree: 9, semitones: 12);
        /// Diminished tenth.
        public static Interval d10 = new Interval(QualityType.Diminished, degree: 10, semitones: 14);
        /// Diminished eleventh.
        public static Interval d11 = new Interval(QualityType.Diminished, degree: 11, semitones: 16);
        /// Diminished twelfth.
        public static Interval d12 = new Interval(QualityType.Diminished, degree: 12, semitones: 18);
        /// Diminished thirteenth.
        public static Interval d13 = new Interval(QualityType.Diminished, degree: 13, semitones: 19);
        /// Diminished fourteenth.
        public static Interval d14 = new Interval(QualityType.Diminished, degree: 14, semitones: 21);
        /// Diminished fifteenth.
        public static Interval d15 = new Interval(QualityType.Diminished, degree: 15, semitones: 23);

        /// Augmented first.
        public static Interval A1 = new Interval(QualityType.Augmented, degree: 1, semitones: 1);
        /// Augmented second.
        public static Interval A2 = new Interval(QualityType.Augmented, degree: 2, semitones: 3);
        /// Augmented third.
        public static Interval A3 = new Interval(QualityType.Augmented, degree: 3, semitones: 5);
        /// Augmented fourth.
        public static Interval A4 = new Interval(QualityType.Augmented, degree: 4, semitones: 6);
        /// Augmented fifth.
        public static Interval A5 = new Interval(QualityType.Augmented, degree: 5, semitones: 8);
        /// Augmented sixth.
        public static Interval A6 = new Interval(QualityType.Augmented, degree: 6, semitones: 10);
        /// Augmented seventh.
        public static Interval A7 = new Interval(QualityType.Augmented, degree: 7, semitones: 12);
        /// Augmented octave.
        public static Interval A8 = new Interval(QualityType.Augmented, degree: 8, semitones: 13);
        /// Augmented ninth.
        public static Interval A9 = new Interval(QualityType.Augmented, degree: 9, semitones: 15);
        /// Augmented tenth.
        public static Interval A10 = new Interval(QualityType.Augmented, degree: 10, semitones: 17);
        /// Augmented eleventh.
        public static Interval A11 = new Interval(QualityType.Augmented, degree: 11, semitones: 18);
        /// Augmented twelfth.
        public static Interval A12 = new Interval(QualityType.Augmented, degree: 12, semitones: 20);
        /// Augmented thirteenth.
        public static Interval A13 = new Interval(QualityType.Augmented, degree: 13, semitones: 22);
        /// Augmented fourteenth.
        public static Interval A14 = new Interval(QualityType.Augmented, degree: 14, semitones: 24);
        /// Augmented fifteenth.
        public static Interval A15 = new Interval(QualityType.Augmented, degree: 15, semitones: 25);

        /// All pre-defined intervals in a static array. You can filter it out with qualities, degrees or semitones.
        public static Interval[] All = {
    P1, P4, P5, P8, P11, P12, P15,
    m2, m3, m6, m7, m9, m10, m13, m14,
    M2, M3, M6, M7, M9, M10, M13, M14,
    d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15,
    A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15,
        };

        // MARK: CustomStringConvertible

        /// Returns the notation of the interval.
        public string Notation => $"{Quality.Notation()}{Degree}";

        /// Returns the name of the interval.
        public string Description => $"{Quality} {Degree}";


        //// MARK: Hashable

        //public func hash(into hasher: inout Hasher) {
        //  hasher.combine(quality)
        //  hasher.combine(degree)
        //  hasher.combine(semitones)
        //}
    }
}