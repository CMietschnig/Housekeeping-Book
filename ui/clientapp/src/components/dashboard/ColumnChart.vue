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
  dataLabels: {
    enabled: true,
    style: {
      fontSize: '12px',
      colors: ['#fff']
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
    }
  },
  plotOptions: {
    bar: {
      borderRadius: 6,
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
      color: '#444'
    }
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

<style scoped>
#chart,
.apexcharts-tooltip {
  color: #000000;
}

#chart,
.apexcharts-tooltip .apexcharts-tooltip-series-group.active {
  background: #ffffff !important;
}
</style>
