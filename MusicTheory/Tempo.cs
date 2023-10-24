//
//  Tempo.swift
//  MusicTheory
//
//  Created by Cem Olcay on 21.06.2018.
//  Copyright © 2018 cemolcay. All rights reserved.
//
//  https://github.com/cemolcay/MusicTheory
//
using System;
namespace MusicTheory
{

    /// Defines the tempo of the music with beats per second and time signature.
    public class Tempo//: Codable, Hashable, CustomStringConvertible {
    {
        /// Time signature of music.
        public TimeSignature TimeSignature { get; set; }
        /// Beats per minutes.
        public double Bpm { get; set; }

        /// Initilizes tempo with time signature and BPM.
        ///
        /// - Parameters:
        ///   - timeSignature: Time Signature.
        ///   - bpm: Beats per minute.
        public Tempo(TimeSignature timeSignature, double bpm = 120.0)
        {
            TimeSignature = timeSignature;
            Bpm = bpm;
        }

        /// Caluclates the duration of a note value in seconds.
        public TimeSpan Duration(NoteValue noteValue)
        {
            var secondsPerBeat = 60.0 / Bpm;
            return TimeSpan.FromSeconds(secondsPerBeat * (TimeSignature.NoteValue.Rate() / noteValue.Type.Rate()) * noteValue.Modifier.ToDouble());
        }

        /// Calculates the note length in samples. Useful for sequencing notes sample accurate in the DSP.
        ///
        /// - Parameters:
        ///   - noteValue: Rate of the note you want to calculate sample length.
        ///   - sampleRate: Number of samples in a second. Defaults to 44100.
        /// - Returns: Returns the sample length of a note value.
        public double SampleLength(NoteValue noteValue, double sampleRate)
        {
            var secondsPerBeat = 60.0 / Bpm;
            return secondsPerBeat * sampleRate * ((4 / noteValue.Type.Rate()) * noteValue.Modifier.ToDouble());
        }

        /// Calculates the LFO speed of a note vaule in hertz.
        public double Hertz(NoteValue noteValue)
        {
            return 1.0 / Duration(noteValue).TotalSeconds;
        }

        // MARK: Equatable

        /// Compares two Tempo instances and returns if they are identical.
        /// - Parameters:
        ///   - lhs: Left hand side of the equation.
        ///   - rhs: Right hand side of the equation.
        /// - Returns: Returns true if two instances are identical.
        public static bool operator ==(Tempo lhs, Tempo rhs)
        {
            return lhs.GetHashCode() == rhs.GetHashCode();
        }
        public static bool operator !=(Tempo lhs, Tempo rhs)
        {
            return lhs.GetHashCode() != rhs.GetHashCode();
        }

        // MARK: CustomStringConvertible
        public override string ToString()
        {
            return $"{Bpm}";
        }

        public override bool Equals(object obj)
        {
            
            return base.Equals(obj);
        }
    }
}
