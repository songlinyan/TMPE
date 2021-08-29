using GenericGameBridge.Service;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericGameBridge.Service {
    /// <summary>
    /// Mutable struct enumerator to be returned from the GetSegmentLaneIds method.
    /// This implementation is just for perf optimizations and should be handled with care since it is a mutable struct!
    /// This should be fine for the regular foreach use case, but could cause bugs if used for anything else inappropriately.
    /// </summary>
    public struct GetSegmentLaneIdsEnumerator : IEnumerator<LaneIdAndLaneIndex> {
        private uint _initialLaneId;
        private int _netInfoLanesLength;
        private NetLane[] _laneBuffer;

        private bool _firstRun;
        private LaneIdAndLaneIndex _currentLaneIdAndLaneIndex;

        public GetSegmentLaneIdsEnumerator(uint initialLaneId, int netInfoLanesLength, NetLane[] laneBuffer) {
            _initialLaneId = initialLaneId;
            _netInfoLanesLength = netInfoLanesLength;
            _laneBuffer = laneBuffer ?? throw new ArgumentNullException(nameof(laneBuffer));

            _firstRun = true;

            _currentLaneIdAndLaneIndex = new LaneIdAndLaneIndex(uint.MaxValue, -1);
        }

        public LaneIdAndLaneIndex Current => _currentLaneIdAndLaneIndex;

        object IEnumerator.Current => _currentLaneIdAndLaneIndex;

        public bool MoveNext() {
            if (_initialLaneId == 0 || _netInfoLanesLength < 1) {
                return false;
            }

            if (_firstRun) {
                _firstRun = false;
                _currentLaneIdAndLaneIndex = new LaneIdAndLaneIndex(_initialLaneId, 0);
                return true;
            }

            _currentLaneIdAndLaneIndex = new LaneIdAndLaneIndex(
                _laneBuffer[_currentLaneIdAndLaneIndex.laneId].m_nextLane,
                _currentLaneIdAndLaneIndex.laneIndex + 1);

            return _currentLaneIdAndLaneIndex.laneId != 0
                && _currentLaneIdAndLaneIndex.laneIndex < _netInfoLanesLength;
        }

        public void Reset() {
            _firstRun = true;
            _currentLaneIdAndLaneIndex = new LaneIdAndLaneIndex(uint.MaxValue, -1);
        }

        public void Dispose() {
        }
    }
}
