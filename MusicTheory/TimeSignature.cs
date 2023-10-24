//
//  TimeSignature.swift
//  MusicTheory iOS
//
//  Created by Cem Olcay on 21.06.2018.
//  Copyright © 2018 cemolcay. All rights reserved.
//
//  https://github.com/cemolcay/MusicTheory
//

namespace MusicTheory
{

    /// Defines how many beats in a measure with which note value.
    public class TimeSignature //: Codable, Hashable, CustomStringConvertible
    {
        /// Beats per measure.
        public int Beats { get; set; }
        /// Note value per beat.
        public NoteValueType NoteValue { get; set; }

        /// Initilizes the time signature with beats per measure and the value of the notes in beat.
        ///
        /// - Parameters:
        ///   - beats: Number of beats in a measure
        ///   - noteValue: Note value of the beats.
        public TimeSignature(int beats = 4, NoteValueType noteValue = NoteValueType.Quarter)
        {
            Beats = beats;
            NoteValue = noteValue;
        }

        // MARK: CustomStringConvertible

        public override string ToString()
        {

            return $"{Beats}/{NoteValue.Rate()}))";
        }
    }
}