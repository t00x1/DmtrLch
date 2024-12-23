import React from 'react';
import { Wheel } from 'react-custom-roulette';
const CasinoWheelElem = ({mustSpin, prizeNumber, setMustSpin}) => {
  const Data = Array.from({ length: 37 }, (_, i) => ({
    option: i.toString(),
    style: {
      backgroundColor: i === 0 ? 'white' : i % 2 === 0 ? 'black' : '#521861',
      textColor: i === 0 ? 'black' : i % 2 === 0 ? 'white' : 'white',
    },
  }));
  return (
    <div>
      <Wheel
                mustStartSpinning={mustSpin}
                prizeNumber={prizeNumber}
                data={Data}
                // backgroundColors={['#3e3e3e', '#df3428']}
                textColors={['#ffffff']}
                onStopSpinning={() => {
                  setMustSpin(false);
                }}
                innerRadius={30}
                outerBorderWidth={30}
                outerBorderColor="#0000"
                innerBorderWidth={5}
                innerBorderColor="black"
                radiusLineWidth={3}
                radiusLineColor=""
                fontSize={20}
                perpendicularText={true}
                textDistance={90}
                spinDuration={2}
              />
    </div>
      
  );
}

export default CasinoWheelElem;
