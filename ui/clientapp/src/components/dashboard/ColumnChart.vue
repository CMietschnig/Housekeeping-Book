<script setup lang="ts">

const props = defineProps({
  series: {
    type: Array<number>,
    required: true
  },
  categories: {
    type: Array<string>,
    required: true
  },
  chartTitle: {
    type: String,
    required: true
  },
  seriesTitle: {
    type: String,
    required: true
  }
})

const chartOptions = {
  chart: {
    background: 'transparent'
  },
  legend: {
    show: false
  },
  fill: {
    colors: ['#e1b80d'],
    opacity: 0.9,
    type: 'solid',
    gradient: {
      shade: 'dark',
      type: 'horizontal',
      shadeIntensity: 0.5,
      gradientToColors: ['#e1b80d'],
      inverseColors: true,
      opacityFrom: 0,
      opacityTo: 1,
      stops: [0, 50, 100],
      colorStops: []
    }
  },
  grid: {
    show: true,
    borderColor: '#d5c3aa',
    strokeDashArray: 0,
    position: 'back',
    xaxis: {
      lines: {
        show: false
      }
    },
    yaxis: {
      lines: {
        show: true
      }
    },
    padding: {
      top: 0,
      right: 20,
      bottom: 5,
      left: 10
    }
  },
  dataLabels: {
    enabled: true,
    style: {
      fontSize: '12px',
      colors: ['#867666']
    }
  },
  xaxis: {
    categories: props.categories,
    position: 'top',
    axisBorder: {
      show: false
    },
    axisTicks: {
      show: false
    },
    crosshairs: {
      fill: {
        type: 'gradient',
        gradient: {
          colorFrom: '#D8E3F0',
          colorTo: '#BED1E6',
          stops: [0, 100],
          opacityFrom: 0.4,
          opacityTo: 0.5
        }
      }
    },
    tooltip: {
      enabled: false
    },
    labels: {
      style: {
        colors: '#867666'
      }
    }
  },
  yaxis: {
    labels: {
      style: {
        colors: '#867666'
      }
    }
  },
  plotOptions: {
    bar: {
      distributed: true,
      borderRadius: 3,
      dataLabels: {
        position: 'top'
      }
    }
  },
  title: {
    text: props.chartTitle,
    floating: true,
    offsetY: 230,
    align: 'center',
    style: {
      color: '#867666'
    }
  },
  tooltip: {
    enabled: false
  }
}
</script>

<template>
  <div id="chart">
    <apexchart
      type="bar"
      height="250"
      :options="chartOptions"
      :series="[{ name: seriesTitle, data: series }]"
    ></apexchart>
  </div>
</template>
